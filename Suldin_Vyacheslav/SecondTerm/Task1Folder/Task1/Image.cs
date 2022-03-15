using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
	public class Image
	{
		public string Name;

		public byte[] Type = new byte[2];
		public int Size;
		public int Somes;
		public int Offset;
		public int HeaderSize;
		public int Widht;
		public int Height;
		public Int16 Colors;
		public Int16 Bits;
		public byte[] Least = new byte[24];
		public byte[,] Matrix;
		public Image(string name)
		{
			Name = name;
			try
			{
				using (var reader = new BinaryReader(File.Open(this.Name, FileMode.Open)))
				{
					Type[0] = reader.ReadByte();
					Type[1] = reader.ReadByte();
					Size = reader.ReadInt32();
					Somes = reader.ReadInt32();
					Offset = reader.ReadInt32();
					HeaderSize = reader.ReadInt32();
					Widht = reader.ReadInt32();
					Height = reader.ReadInt32();
					Colors = reader.ReadInt16();
					Bits = reader.ReadInt16();
					for (int i = 0; i < 24; i++)
					{
						Least[i] = reader.ReadByte();
					}

					Matrix = new byte[Height, Widht * Bits / 8];
					for (int i = 0; i < Height; i++)
					{
						for (int j = 0; j < Widht * Bits / 8; j++)
						{
							Matrix[i, j] = reader.ReadByte();
						}
					}
				}
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("FileNotFoundException");
				Console.ReadLine();
			}

			catch (ArgumentException)
			{
				Console.WriteLine("ArgumentException");
				Console.ReadLine();
			}
		}

		public void ApplyFilter(double[,] filter, string newImageName)
		{
			byte[,] matrix = new byte[Height, Widht * Bits / 8];

			if (filter == null)
			{
				Console.WriteLine("FilterNotFoundException");
				return;
			}
			using (var writer = new BinaryWriter(File.Open(newImageName, FileMode.Create)))
			{
				writer.Write(Type);
				writer.Write(Size);
				writer.Write(Somes);
				writer.Write(Offset);
				writer.Write(HeaderSize);
				writer.Write(Widht);
				writer.Write(Height);
				writer.Write(Colors);
				writer.Write(Bits);
				writer.Write(Least);
				int bits = Bits / 8;
				int coreSizeX = filter.GetLength(1);
				int coreSizeY = filter.GetLength(0);
				switch (coreSizeY)
				{
					case 1:
						{
							switch (coreSizeX)
							{
								case 1:
									{

										for (int i = 1; i < Height - 1; i++)
											for (int j = bits; j < Widht * bits - bits; j += bits)
												for (int n = 0; n < 3; n++)
												{
													int[] mask = new int[9];
													int count = 0;
													for (int p = 0; p < 3; p++)
														for (int p1 = 0; p1 < 3; p1++)
														{
															mask[count] = (int)Matrix[i - 1 + p, j - bits + bits * p1 + n];
															count++;
														}
													Array.Sort(mask);
													matrix[i, j + n] = (byte)mask[4];
												}
										for (int i = 0; i < Height; i++)
										{
											for (int j = 0; j < Widht * bits; j += bits)
											{
												for (int p = 0; p < bits; p++)
													writer.Write(matrix[i, j + p]);
											}
										}
										return;

									}
								case 3:
									{
										for (int i = 0; i < Height; i++)
										{
											for (int j = 0; j < Widht * bits; j += bits)
											{
												byte pixel =
													(byte)(Matrix[i, j] * filter[0, 0] +
													Matrix[i, j + 1] * filter[0, 1] +
													Matrix[i, j + 2] * filter[0, 2]);

												for (int p = 0; p < bits; p++)
													writer.Write(pixel);
											}
										}
										return;
									}
							}
							return;
						}
					default:
						{

							int imp = (int)(coreSizeX / 2 + 1) - coreSizeX;

							int k = 0, sensivity = 0;

							if (filter[0, 0] > 0 && filter[0, 0] < 1) k = 2;

							if (filter[2, 2] == 1) sensivity = 60;
							else sensivity = 250;

							double[,] tmp = new double[Height, Widht * bits];

							for (int i = (-1) * imp; i < Height + imp; i++)
								for (int j = (-1) * imp * bits; j < Widht * bits + imp * bits; j += bits)
									for (int n = 0; n < 1 + k; n++)
									{
										tmp[i, j + n] = 0;
										int sumX = 0, sumY = 0;
										for (int y = 0; y < coreSizeX; y++)
											for (int x = 0; x < coreSizeX; x++)
											{
												if (k == 2) tmp[i, j + n] += filter[y, x] * (int)Matrix[i + imp + y, j + bits * (imp + x) + n];
												else
												{
													sumX += (int)(filter[y, x] * Matrix[i + imp + y, j + bits * (imp + x) + n]);
													sumY += (int)(filter[coreSizeY != 3 ? y + 3 : y, x] * Matrix[i + imp + y, j + bits * (imp + x) + n]);
												}
											}
										if (k == 2) continue;
										else if (filter[0, 1] == 0 && coreSizeY == 3) tmp[i, j] = Math.Abs(sumX) < sensivity ? 0 : 255;
										else if (filter[1, 2] == 0) tmp[i, j] = Math.Abs(sumY) < sensivity ? 0 : 255;
										else tmp[i, j] = Math.Sqrt(sumX * sumX + sumY * sumY) < sensivity ? 0 : 255;
									}

							for (int i = 0; i < Height; i++)
							{
								for (int j = 0; j < Widht * bits; j += bits)
								{
									if (i >= (-1) * imp && j >= (-1) * imp * bits)
									{
										for (int n = 0; n < 3; n++) writer.Write((byte)tmp[i, j + k * n / 2]);
									}
									else
									{
										for (int n = 0; n < 3; n++) writer.Write(Matrix[i, j + n]);
									}
								}
							}
							return;
						}
				}
			}
		}

		public static bool operator ==(Image firstImage, Image secondImage)
		{
			if (((firstImage.Type).Except(secondImage.Type).ToArray()).Length != 0 ||
			firstImage.Size != secondImage.Size ||
			firstImage.Somes != secondImage.Somes ||
			firstImage.Offset != secondImage.Offset ||
			firstImage.HeaderSize != secondImage.HeaderSize ||
			firstImage.Widht != secondImage.Widht ||
			firstImage.Height != secondImage.Height ||
			firstImage.Colors != secondImage.Colors ||
			firstImage.Bits != secondImage.Bits ||
			((firstImage.Least).Except(secondImage.Least).ToArray()).Length != 0)
				return false;
			
			
			for (int i = 0; i < firstImage.Height; i++)
			{
				for (int j = 0; j < firstImage.Widht*firstImage.Bits/8; j++)
				{
					if (firstImage.Matrix[i, j] != secondImage.Matrix[i, j])
						return false;
				}
			}
			return true;
		}
		public static bool operator !=(Image firstImage, Image secondImage)
		{
			return !(firstImage == secondImage);
		}
	}
}
