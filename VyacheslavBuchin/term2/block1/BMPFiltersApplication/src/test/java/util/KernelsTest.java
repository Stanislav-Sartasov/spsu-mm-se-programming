package util;

import kernel.KernelItem;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class KernelsTest {

    @Nested
    class SymmetricalRectangleKernelTest {

        @Test
        void MethodShouldThrowExceptionIfIncorrectNumberOfCoefficientsGiven() {
            var thrown = assertThrows(IllegalArgumentException.class,
                    () -> Kernels.symmetricalRectangleKernel(1, 1, 2, 2));
            assertEquals(thrown.getMessage(), "Number of coefficients must be equal to width * height");
        }

        @Test
        void MethodShouldThrowExceptionIfGivenRectangleIsNotSymmetrical() {
            var thrown = assertThrows(IllegalArgumentException.class,
                    () -> Kernels.symmetricalRectangleKernel(2, 1, 2, 2));
            assertEquals(thrown.getMessage(), "Width and height must be odd numbers");
            thrown = assertThrows(IllegalArgumentException.class,
                    () -> Kernels.symmetricalRectangleKernel(1, 2, 2, 2));
            assertEquals(thrown.getMessage(), "Width and height must be odd numbers");
        }

        @Test
        void MethodShouldReturnCorrectKernel() {
            var kernel = Kernels.symmetricalRectangleKernel(3, 3,
                    1, 2, 3,
                    4, 5, 6,
                    7, 8, 9);

            var items = kernel.getItems();
            assertEquals(items[0], new KernelItem(-1, 1, 1));
            assertEquals(items[1], new KernelItem(0, 1, 2));
            assertEquals(items[2], new KernelItem(1, 1, 3));
            assertEquals(items[3], new KernelItem(-1, 0, 4));
            assertEquals(items[4], new KernelItem(0, 0, 5));
            assertEquals(items[5], new KernelItem(1, 0, 6));
            assertEquals(items[6], new KernelItem(-1, -1, 7));
            assertEquals(items[7], new KernelItem(0, -1, 8));
            assertEquals(items[8], new KernelItem(1, -1, 9));
        }
    }

    @Test
    void ConstSymmetricalRectangleKernelShouldReturnCorrectValue() {
        var items1 = Kernels.constSymmetricalRectangleKernel(3, 3, 1).getItems();
        var items2 = Kernels.symmetricalRectangleKernel(3, 3,
                1, 1, 1,
                1, 1, 1,
                1, 1, 1).getItems();
        for (int i = 0; i < 9; i++)
            assertEquals(items1[i], items2[i]);
    }
}