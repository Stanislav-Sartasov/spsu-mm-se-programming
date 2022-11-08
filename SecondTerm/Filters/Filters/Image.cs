using System.IO;

namespace Filters
{
	public class Image
	{
		private struct BitMapFileHeader
		{
			internal ushort bfType;
			internal uint bfSize;
			internal ushort bfReserved1;
			internal ushort bfReserved2;
			internal uint bfOffBits;
		}

		private struct BitMapInfoHeader
		{
			internal uint biSize;
			internal uint biWidth;
			internal uint biHeight;
			internal ushort biPlanes;
			internal ushort biBitCount;
			internal uint biCompression;
			internal uint biSizeImage;
			internal uint biXPelsPerMeter;
			internal uint biYPelsPerMeter;
			internal uint biClrUsed;
			internal uint biClrImportant;
		}

		private BitMapFileHeader head = new();
		private BitMapInfoHeader info = new();
		public byte[] mas;

		public void Read(string args)
		{
			FileStream fs = File.OpenRead(args);
			BinaryReader picture = new(fs);

			head.bfType = picture.ReadUInt16();
			head.bfSize = picture.ReadUInt32();
			head.bfReserved1 = picture.ReadUInt16();
			head.bfReserved2 = picture.ReadUInt16();
			head.bfOffBits = picture.ReadUInt32();

			info.biSize = picture.ReadUInt32();
			info.biWidth = picture.ReadUInt32();
			info.biHeight = picture.ReadUInt32();
			info.biPlanes = picture.ReadUInt16();
			info.biBitCount = picture.ReadUInt16();
			info.biCompression = picture.ReadUInt32();
			info.biSizeImage = picture.ReadUInt32();
			info.biXPelsPerMeter = picture.ReadUInt32();
			info.biYPelsPerMeter = picture.ReadUInt32();
			info.biClrUsed = picture.ReadUInt32();
			info.biClrImportant = picture.ReadUInt32();

			mas = new byte[info.biSizeImage];
			for (int i = 0; i < info.biSizeImage; i++)
			{
				mas[i] = picture.ReadByte();
			}
		}

		private void Write(string[] args)
		{
			FileStream fs = File.Create(args[3]);
			BinaryWriter picture = new(fs);

			picture.Write(head.bfType);
			picture.Write(head.bfSize);
			picture.Write(head.bfReserved1);
			picture.Write(head.bfReserved2);
			picture.Write(head.bfOffBits);

			picture.Write(info.biSize);
			picture.Write(info.biWidth);
			picture.Write(info.biHeight);
			picture.Write(info.biPlanes);
			picture.Write(info.biBitCount);
			picture.Write(info.biCompression);
			picture.Write(info.biSizeImage);
			picture.Write(info.biXPelsPerMeter);
			picture.Write(info.biYPelsPerMeter);
			picture.Write(info.biClrUsed);
			picture.Write(info.biClrImportant);

			picture.Write(mas);
		}

		public void ImageProcessing(string[] args)
		{
			Read(args[1]);
			Filter[] filters = {new GreyScale(), new Negativ(), new Averaging(), new Gauss(), new SobelX(), new SobelY()};
			foreach (Filter filter in filters)
			{
				if (args[2] == filter.GetType().Name)
				{
					filter.Filtering(mas, info.biWidth, info.biHeight);
				}
			}
			Write(args);
		}
	}
}
