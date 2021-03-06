using System;

namespace twp
{
	namespace app
	{
		public class typedef{
			static public int BIT(int x){
				return 1 << x;
			}
		}
		public class Position
		{
			public int x;
			public int y;
			
			public void FromBin(NetSocket.ByteArray bin)
			{
				bin.Get_ (out x);
				bin.Get_ (out y);
			}
			
			public byte[] ToBin()
			{
				NetSocket.ByteArray bin = new NetSocket.ByteArray();
				bin.Put (x);
				bin.Put (y);
				return bin.GetData();
			}
		}
		
		public class Direction
		{
			public float x;
			public float y;
			
			public void FromBin(NetSocket.ByteArray bin)
			{
				bin.Get_ (out x);
				bin.Get_ (out y);
			}
			
			public byte[] ToBin()
			{
				NetSocket.ByteArray bin = new NetSocket.ByteArray();
				bin.Put (x);
				bin.Put (y);
				return bin.GetData();
			}
		};
		
	}
}

