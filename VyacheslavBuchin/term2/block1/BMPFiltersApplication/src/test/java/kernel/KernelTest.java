package kernel;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class KernelTest {

    private Kernel kernel;
    private KernelItem[] items;

    @BeforeEach
    void setUp() {
        items = new KernelItem[0];
        kernel = new Kernel(items);
    }

    @Test
    void GetItemsShouldReturnCorrectValue() {
        assertEquals(kernel.getItems(), items);
    }
}