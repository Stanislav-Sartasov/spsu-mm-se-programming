package kernel;

public class Kernel {
	protected final KernelItem[] items;

	public Kernel(KernelItem[] items) {
		this.items = items;
	}

	public KernelItem[] getItems() {
		return items;
	}

}
