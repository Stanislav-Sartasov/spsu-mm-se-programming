using Task_1.Core.Image;

namespace Task_1.Core.Filter.Interfaces
{
	public interface IFilter
	{
		public Bitmap ApplyTo(Bitmap bitmap);
	}
}
