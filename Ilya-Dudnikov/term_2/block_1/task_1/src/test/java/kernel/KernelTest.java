package kernel;

import Kernel.Kernel;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class KernelTest {

	@Test
	void createKernelSuccess() {
		assertDoesNotThrow(
				() -> {
					Kernel.createKernel(new double[][] {
							{ 1, 1, 1 },
							{ 1, 1, 1 },
							{ 1, 1, 1 }
					});
				}
		);
	}

	@Test
	void createKernelFail1() {
		assertThrows(
				IllegalArgumentException.class,
				() -> {
					Kernel.createKernel(new double[][] {
							{ 1, 1, 1 },
							{ 1, 1, 1 }
					});
				}
		);
	}

	@Test
	void createKernelFail2() {
		assertThrows(
				IllegalArgumentException.class,
				() -> {
					Kernel.createKernel(new double[][] {
							{ 1, 1 },
							{ 1, 1 }
					});
				}
		);
	}

	@Test
	void createKernelFail3() {
		assertThrows(
				IllegalArgumentException.class,
				() -> {
					Kernel.createKernel(new double[][] {
							{ 1, 1, 1 },
							{ 1, 1 },
							{ 1, 1, 1 }
					});
				}
		);
	}

	@Test
	void getKernelValueAt() {
		Kernel kernel = Kernel.createKernel(new double[][] {
				{ 1, 1, 1 },
				{ 1, 2, 13 },
				{ 1, 1, 1 }
		});

		assertEquals(13, kernel.getKernelValueAt(1, 2));
	}
}