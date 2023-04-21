package BashProject.util.StreamGobbler;

import java.io.*;

public class StreamGobbler extends Thread {
	private InputStream inputStream;
	private PrintStream outputStream;

	public StreamGobbler(InputStream inputStream, PrintStream outputStream) {
		this.inputStream = inputStream;
		this.outputStream = outputStream;
	}

	@Override
	public void run() {
		try {
			InputStreamReader inputStreamReader = new InputStreamReader(inputStream);
			BufferedReader bufferedReader = new BufferedReader(inputStreamReader);
			String currentLine = null;

			while ((currentLine = bufferedReader.readLine()) != null) {
				outputStream.println(currentLine);
			}
		} catch (IOException e) {
			throw new RuntimeException(e);
		}
	}
}
