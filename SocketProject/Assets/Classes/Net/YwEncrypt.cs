//#define Encode
using System;
using System.Collections;
using System.Collections.Generic;
using NetSocket;

namespace yw
{

	public class YwEncrypt
	{
		public const int LIMIT_KEY_LENGTH = 32;
		public const int MSG_MAX_STC_PACK_LEN = 40000;
		
		static public string EncryptKey = "abcdefghijklmnoqprstuvwxyz()_+|1";
	
		static private int _Decode(int rand_key, byte [] dst, byte [] src)
		{
			ByteArray key = new ByteArray(EncryptKey.ToCharArray());
			ByteArray _dst = new ByteArray(dst);
			ByteArray _src = new ByteArray(src);
			
			int srclen = src.Length;
			int k = 0;
			int int_num = srclen - (srclen % sizeof(int));
			int i = 0;
			for(; i < (int)srclen;)
			{
				if(i < int_num)
				{
					//		"机密算法"
					i += sizeof(int);
				}
				else
				{
					//		"机密算法"	
					i += sizeof(byte);
				}
				k = i % LIMIT_KEY_LENGTH;
			}
			
			_dst.CopyTo(dst);
			
			return i;
		}

		static public int Encode(int pack_num, byte[] dst, byte[] src)
		{
			//ByteArray buf = new ByteArray();
			//buf.Put(pack_num);
			//buf.Put(new byte[MSG_MAX_STC_PACK_LEN - sizeof(int)]);
#if Encode			
			byte[] data = new byte[MSG_MAX_STC_PACK_LEN];
			int ret_size = (int)_Decode(pack_num, data, src) + (int)sizeof(int);
			ByteArray buf = new ByteArray();	
			int _pack_key = pack_num ^ 5;
			buf.Put(_pack_key);
			buf.Put(data, ret_size);		
			buf.CopyTo(dst);
#else
			int ret_size = src.Length;		
			ByteArray buf = new ByteArray();
			buf.Put(src);
			buf.CopyTo(dst);
#endif
			return ret_size;
			
			
			//EncryptInfo *encinfo = static_cast<EncryptInfo *>(buf);
			//encinfo->rand_key = pack_num;
	
			//int ret_size = _Decode(encinfo->rand_key, encinfo->data, (const char *)src, srclen) + sizeof(encinfo->rand_key);
			//encinfo->rand_key^=5;
		
			//*dst = s_buff_send;
		
			//return ret_size;
		}
		
		static public int Decode(byte[] dst, byte[] src)
		{
			ByteArray src_buf = new ByteArray(src);
#if Encode
			int rand_key = BitConverter.ToInt32(src, 0);
			return _Decode(rand_key, dst, src_buf.GetRange(sizeof(int)));
#else
			src_buf.CopyTo(dst);
			return src.Length;
#endif
			//const EncryptInfo *encinfo = static_cast<const EncryptInfo *>(src);
		
			//客户端发来的随机key是一个封包顺序号，但是做了简单加密，这里需要解密
			//int rand_key = encinfo->rand_key;
			//*outptr = s_buff_recv;
		
			//return _Decode(rand_key, s_buff_recv, encinfo->data, srclen - sizeof(encinfo->rand_key));
		}
		
		static public void ResetKey()
		{
			EncryptKey = "机密信息";
		}
		
		static public void SetKey(byte[] data)
		{
			string newKey = ("");
			for (int i=0; i<data.Length; ++i)
			{
				char c = (char)data[i];
				newKey += c;
			}
			EncryptKey = newKey;
		}
	}
}