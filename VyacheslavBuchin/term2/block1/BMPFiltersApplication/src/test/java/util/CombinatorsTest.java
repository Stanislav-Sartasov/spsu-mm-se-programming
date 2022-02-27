package util;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.stream.DoubleStream;

import static org.junit.jupiter.api.Assertions.*;

class CombinatorsTest {

	private DoubleStream stream;

	@BeforeEach
	void setUp() {
		stream = DoubleStream.of(2, 3, 25);
	}

	@Test
	void SumTest() {
		assertEquals(30, Combinators.SUM.apply(stream));
	}

	@Test
	void MaxTest() {
		assertEquals(25, Combinators.MAX.apply(stream));
	}

	@Test
	void MinTest() {
		assertEquals(2, Combinators.MIN.apply(stream));
	}
}