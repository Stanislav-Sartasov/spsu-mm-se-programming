import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.net.URLClassLoader;
import java.util.ArrayList;
import java.util.jar.JarFile;

import static org.junit.jupiter.api.Assertions.*;

class BotLoaderTest {
	private BotLoader botLoader;
	private final String RESOURCE_PATH = "src/test/resources/";

	@BeforeEach
	void setUp() throws IOException {
		botLoader = BotLoader.createBotLoader(RESOURCE_PATH);
	}

	@Test
	void loadBotsFromJarFileExpectCorrectLoading() throws IOException {
		var botList = botLoader.loadBotsFromJarFile(new JarFile(RESOURCE_PATH + "bots.jar"));

		assertEquals(4, botList.size());

		ArrayList<String> expected = new ArrayList<>();
		expected.add("BasicBot");
		expected.add("sample_package.AnotherBasicBot");
		expected.add("DealerBot");
		expected.add("RandomBot");

		expected.sort(String::compareTo);
		ArrayList<String> botNameList = new ArrayList<>();

		for (var bot : botList)
			botNameList.add(bot.getPlayer().getId());

		botNameList.sort(String::compareTo);
		assertEquals(expected, botNameList);
	}

	@Test
	void loadBotsFromJarFileJarWithNoBotsExpectEmptyList() throws IOException {
		assertEquals(0, botLoader.loadBotsFromJarFile(new JarFile(RESOURCE_PATH + "sample_dir/lib.jar")).size());
	}

	@Test
	void loadJarFilesFromPathExpectThreeJars() throws IOException {
		var jarFiles = botLoader.loadJarFilesFromPath(RESOURCE_PATH);

		assertEquals(3, jarFiles.size());

		ArrayList<String> expected = new ArrayList<>();
		expected.add((RESOURCE_PATH + "bots.jar").replace('/', '\\'));
		expected.add((RESOURCE_PATH + "jar_with_no_bots.jar").replace('/', '\\'));
		expected.add((RESOURCE_PATH + "sample_dir/lib.jar").replace('/', '\\'));

		ArrayList<String> jarFilesNames = new ArrayList<>();
		for (var jarFile : jarFiles)
			jarFilesNames.add(jarFile.getName());

		assertEquals(expected, jarFilesNames);
	}

	@Test
	void loadBotsFromPathExpectFourBots() {
		var botList = botLoader.loadBotsFromPath(RESOURCE_PATH);

		ArrayList<String> expected = new ArrayList<>();
		expected.add("BasicBot");
		expected.add("sample_package.AnotherBasicBot");
		expected.add("DealerBot");
		expected.add("RandomBot");

		expected.sort(String::compareTo);
		ArrayList<String> botNameList = new ArrayList<>();

		for (var bot : botList)
			botNameList.add(bot.getPlayer().getId());

		botNameList.sort(String::compareTo);
		assertEquals(expected, botNameList);
	}
}