import bmp.BMPFile;
import filter_applier.FilterApplier;
import filter_applier.IFilterApplier;
import io.BMPInputStream;
import io.BMPOutputStream;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;
import service.FilterService;
import service.SimpleFilterService;
import util.BMPColorMaps;

import java.io.*;

import static org.junit.jupiter.api.Assertions.*;

class FilterApplicationTest {

	@Nested
	class FilterTests {
		private BMPFile file;
		private IFilterApplier applier;
		private FilterService service;

		@BeforeEach
		void setUp() {
			try (var inputStream = new FileInputStream("src/test/resources/test.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream)) {
				file = bmpInputStream.readBMPFile();
				applier = new FilterApplier(file.colorMap());
				service = new SimpleFilterService();
			} catch (IOException ignored) {
			}
		}

		@Test
		void GrayscaleFilterTest() {
			try (var inputStream = new FileInputStream("src/test/resources/test_grayscale.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream)) {
				var correctFile = bmpInputStream.readBMPFile();
				assertTrue(
						BMPColorMaps.equal(
								applier.apply(service.get("grayscale")).toBMPColorMap(),
								correctFile.colorMap()
						));
			} catch (IOException ignored) {
			}
		}

		@Test
		void GaussianFilterTest() {
			try (var inputStream = new FileInputStream("src/test/resources/test_gaussian.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream)) {
				var correctFile = bmpInputStream.readBMPFile();
				assertTrue(
						BMPColorMaps.equal(
								applier.apply(service.get("gaussian")).toBMPColorMap(),
								correctFile.colorMap()
						));
			} catch (IOException ignored) {
			}
		}

		@Test
		void AverageFilterTest() {
			try (var inputStream = new FileInputStream("src/test/resources/test_average.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream)) {
				var correctFile = bmpInputStream.readBMPFile();
				assertTrue(
						BMPColorMaps.equal(
								applier.apply(service.get("average")).toBMPColorMap(),
								correctFile.colorMap()
						));
			} catch (IOException ignored) {
			}
		}

		@Test
		void MaxFilterTest() {
			try (var inputStream = new FileInputStream("src/test/resources/test_max.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream)) {
				var correctFile = bmpInputStream.readBMPFile();
				assertTrue(
						BMPColorMaps.equal(
								applier.apply(service.get("max")).toBMPColorMap(),
								correctFile.colorMap()
						));
			} catch (IOException ignored) {
			}
		}

		@Test
		void MinFilterTest() {
			try (var inputStream = new FileInputStream("src/test/resources/test_min.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream)) {
				var correctFile = bmpInputStream.readBMPFile();
				assertTrue(
						BMPColorMaps.equal(
								applier.apply(service.get("min")).toBMPColorMap(),
								correctFile.colorMap()
						));
			} catch (IOException ignored) {
			}
		}

		@Test
		void SobelXFilterTest() {
			try (var inputStream = new FileInputStream("src/test/resources/test_sobelX.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream)) {
				var correctFile = bmpInputStream.readBMPFile();
				assertTrue(
						BMPColorMaps.equal(
								applier.apply(service.get("sobelX")).toBMPColorMap(),
								correctFile.colorMap()
						));
			} catch (IOException ignored) {
			}
		}

		@Test
		void SobelYFilterTest() {
			try (var inputStream = new FileInputStream("src/test/resources/test_sobelY.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream)) {
				var correctFile = bmpInputStream.readBMPFile();
				assertTrue(
						BMPColorMaps.equal(
								applier.apply(service.get("sobelY")).toBMPColorMap(),
								correctFile.colorMap()
						));
			} catch (IOException ignored) {
			}
		}
	}

	@Nested
	class ApplicationTest {

		private OutputStream errorStream;
		private OutputStream outStream;
		private PrintStream oldErrorStream;
		private PrintStream oldOutStream;

		@BeforeEach
		void setUp() {
			errorStream = new ByteArrayOutputStream();
			oldErrorStream = System.err;
			System.setErr(new PrintStream(errorStream));

			outStream = new ByteArrayOutputStream();
			oldOutStream = System.out;
			System.setOut(new PrintStream(outStream));
		}

		@AfterEach
		void finish() {
			System.setErr(oldErrorStream);
			System.setOut(oldOutStream);
		}

		@Test
		void HelpMessageShouldBePrintedIfArgumentIsFlagHelp() {
			Main.main(new String[]{"--help"});
			assertEquals(Main.HELP_MESSAGE + System.lineSeparator(), outStream.toString());
		}

		@Test
		void ErrorMessageShouldBePrintedIfIncorrectArgumentsGiven() {
			Main.main(new String[]{"incorrect args"});
			assertEquals(Main.INCORRECT_ARGS_MESSAGE + System.lineSeparator(), errorStream.toString());
		}

		@Test
		void NoSuchFilterMessageShouldBePrintedIfGivenFilterIsNotAvailable() {
			Main.main(new String[]{"src/test/resources/test.bmp", "src/test/resources/some_file_out.bmp", "unavailableFilter"});
			assertEquals("No such filter: " + "unavailableFilter" + System.lineSeparator(), errorStream.toString());
		}

		@Test
		void CorrectFileShouldBeCreatedIfArgsIsCorrect() {
			Main.main(new String[]{"src/test/resources/test.bmp", "src/test/resources/test_grayscale_testing.bmp", "grayscale"});
			try (var inputStream = new FileInputStream("src/test/resources/test_grayscale.bmp");
				 var bmpInputStream = new BMPInputStream(inputStream);
				 var correctInputStream = new FileInputStream("src/test/resources/test_grayscale_testing.bmp");
				 var bmpCorrectInputStream = new BMPInputStream(correctInputStream)) {
				var file = bmpInputStream.readBMPFile();
				var correctFile = bmpCorrectInputStream.readBMPFile();
				assertTrue(BMPColorMaps.equal(correctFile.colorMap(), file.colorMap()));
			} catch (IOException ignored) {
			}
		}

	}
}