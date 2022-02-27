package service;

import filter.GrayscaleFilter;
import filter.KernelFilter;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;
import util.Kernels;

import static org.junit.jupiter.api.Assertions.*;

class SimpleFilterServiceTest {

	private FilterService filterService;

	@BeforeEach
	void setUp() {
		filterService = new SimpleFilterService();
	}

	@Test
	void GetShouldReturnCorrectFilterByName() {
		assertEquals(GrayscaleFilter.class, filterService.get("grayscale").getClass());
		assertEquals(KernelFilter.class, filterService.get("gaussian").getClass());
	}

	@Nested
	class ExistsTest {
		@Test
		void ExistsShouldReturnTrueIfFilterExists() {
			assertTrue(filterService.exists("min"));
		}

		@Test
		void ExistsShouldReturnFalseIfFilterDoNotExists() {
			assertFalse(filterService.exists("some filter"));
		}
	}

	@Test
	void AddShouldAddGivenFilterToService() {
		assertFalse(filterService.exists("identity"));
		filterService.add("identity", new KernelFilter(
				Kernels.symmetricalRectangleKernel(1, 1, 1)
		));
		assertTrue(filterService.exists("identity"));
		assertEquals(KernelFilter.class, filterService.get("identity").getClass());
	}
}