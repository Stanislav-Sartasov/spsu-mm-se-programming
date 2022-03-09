using Core.Image;

namespace Core.Filter.Interfaces
{
	public interface IFilter
	{
		public void ApplyTo(Bitmap bitmap);
	}
}
