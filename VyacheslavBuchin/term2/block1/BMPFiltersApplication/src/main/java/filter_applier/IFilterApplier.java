package filter_applier;

import bmp.BMPColorMap;
import filter.IFilter;

public interface IFilterApplier {
	IFilterApplier apply(IFilter filter);

	BMPColorMap toBMPColorMap();
}
