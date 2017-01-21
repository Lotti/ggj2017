﻿using System.Collections;
using UnityEngine;

public class F1_MicStreamer : MonoBehaviour
{
	// listeners attach to this delegate callback
	public delegate  void MicCallbackDelegate( float[] buf );
	public MicCallbackDelegate  floatsInDelegate;

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	// Buffers

	/*
     every frame in FixedUpdate(), we send all new mic data to all listeners
     say e.g. 610 samples have arrived
     the obvious approach is to dynamically allocate 612 floats and copy 612 floats out of the ring buffer
     this is BAD.
     dynamic allocation in real-time audio is BAD.
     
     solution:
     pre-allocate buffers of length 1,2,4,8,…,512,1024
     now 610 =  512 + 64 + 32 + 2
     
     so we could just fill up some of our preallocated POT (power of 2) buffers and make a
             separate delegate callback for each.
     
     furthermore we can decide to hold back up to e.g. 63 samples
     so this frame we sent back 512 + 64,  leaving 34 which will be sent in the next update
     this reduces overhead of function calls;
         
      additionally there is a potential problem that the streamer may change the contents of a
             particular buffer while one of the listeners is chewing on it.
     
     solution here is to create say 8 buffers of each size and cycle through them.
    */

	class POTBuf
	{
		public const int POT_min =  6;      // 2^6 = 64
		public const int POT_max = 10;      // 2^10 = 1024

		const int redundancy = 8;
		int index = 0;

		float[][] internalBuffers = new float[ redundancy ] [];

		public float[] buf {
			get {
				return internalBuffers[ index ];
			}
		}

		public void Cycle( ) {
			index = ( index + 1 ) % redundancy;
		}

		public POTBuf( int POT ) {
			for( int r=0; r < redundancy; r++ ) {
				internalBuffers[ r ] = new float [ 1 << POT ];
			}
		}
	}

	POTBuf [] potBuffers = new POTBuf[ POTBuf.POT_max + 1 ];

	void SetupBuffers( )
	{
		for( int k = POTBuf.POT_min; k <= POTBuf.POT_max; k++ )
			potBuffers[ k ] = new POTBuf( k );
	}  

	// - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	private AudioSource audioS;

	private int readHead = 0;
	private bool isStreaming = false;
	private string MicString = "";

	void Awake(){
		audioS = GetComponent<AudioSource> ();
		var mics = Microphone.devices;
		var s = "Mics: ";
		foreach( var m in mics){
			s += "\n" + m;
		}
		Debug.Log ( s );
	}

	void FixedUpdate( )
	{
		FlushToListeners( );
	}

	void FlushToListeners( )
	{
		int writeHead = Microphone.GetPosition( MicString );

		if( readHead == writeHead  ||  potBuffers == null  ||  ! isStreaming )
			return;

		// Say audio.clip.samples (S)  = 100
		// if w=1, r=0, we want 1 sample.  ( S + 1 - 0 ) % S = 1 YES
		// if w=0, r=99, we want 1 sample.  ( S + 0 - 99 ) % S = 1 YES
		int nFloatsToGet =  ( audioS.clip.samples  +  writeHead - readHead ) % audioS.clip.samples;

		for( int k = POTBuf.POT_max; k >= POTBuf.POT_min; k-- )
		{
			POTBuf B = potBuffers[ k ];

			int n = B.buf.Length; // i.e.  1 << k;

			while( nFloatsToGet >= n )
			{

				// If the read length from the offset is longer than the clip length,
				//   the read will wrap around and read the remaining samples
				//   from the start of the clip.
				audioS.clip.GetData( B.buf, readHead );
				readHead = ( readHead + n ) % audioS.clip.samples;

				if( floatsInDelegate != null )
					floatsInDelegate( B.buf );

				B.Cycle();
				nFloatsToGet -= n;
			}
		}
	}
}