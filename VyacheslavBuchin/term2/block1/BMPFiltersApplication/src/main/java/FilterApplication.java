import bmp.BMPFile;
import filter_applier.FilterApplier;
import io.BMPInputStream;
import io.BMPOutputStream;
import service.FilterService;
import service.SimpleFilterService;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

public class FilterApplication {

	private final InputStream source;
	private final OutputStream destination;
	private final String[] filters;
	private final FilterService filterService = new SimpleFilterService();

	public FilterApplication(InputStream source, OutputStream destination, String... filters) {
		this.source = source;
		this.destination = destination;
		this.filters = filters;
	}

	public void run() {

		try (var bmpInput = new BMPInputStream(source);
			 var bmpOutput = new BMPOutputStream(destination)) {

			var file = bmpInput.readBMPFile();
			var applier = new FilterApplier(file.colorMap());

			for (String filter : filters) {
				if (!filterService.exists(filter)) {
					System.err.println("No such filter: " + filter);
					return;
				}

				applier.apply(filterService.get(filter));
			}

			bmpOutput.writeBMPFile(new BMPFile(file.header(), applier.toBMPColorMap()));

		} catch (IOException e) {
			System.err.println(e.getMessage());
		}

	}

}
