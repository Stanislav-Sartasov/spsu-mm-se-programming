using Task_1.Core.Image;

namespace Task_1.Core.Filter.Interfaces
{
	public interface IFilter
	{
		public void ApplyTo(Bitmap bitmap);
	}
}
