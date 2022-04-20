import player.AbstractPlayer;
import player.BlackjackPlayer;
import player.PlayerController;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.net.URL;
import java.net.URLClassLoader;
import java.util.ArrayList;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

public class BotLoader {
	private ClassLoader classLoader;

	private BotLoader(ClassLoader classLoader) {
		this.classLoader = classLoader;
	}

	public static BotLoader createBotLoader(String path) throws IOException {
		URLClassLoader classLoader = new URLClassLoader(new URL[]{new URL("jar:file:" + path + "!/")});

		return new BotLoader(classLoader);
	}

	private PlayerController loadClass(JarEntry entry) {
		if (!entry.isDirectory() && entry.getName().endsWith(".class")) {
			try {
				String entryName = entry.getName().replace('/', '.').substring(0, entry.getName().length() - 6);
				return (PlayerController) classLoader
						.loadClass(entryName)
						.getDeclaredConstructor(AbstractPlayer.class)
						.newInstance(new BlackjackPlayer(entryName));
			} catch (ClassNotFoundException
					| NoSuchMethodException
					| InvocationTargetException
					| InstantiationException
					| IllegalAccessException e) {
				System.err.println(e.getMessage());
			}
		}

		return null;
	}

	protected ArrayList<JarFile> loadJarFilesFromPath(String path) {
		ArrayList<JarFile> result = new ArrayList<>();
		File file = new File(path);

		for (var fileIterator : file.listFiles()) {
			if (fileIterator.isDirectory()) {
				result.addAll(loadJarFilesFromPath(fileIterator.getPath()));
			} else if (fileIterator.getName().endsWith(".jar")) {
				try {
					result.add(new JarFile(fileIterator.getPath()));
				} catch (IOException e) {
					System.err.println(e.getMessage());
				}
			}
		}
		return result;
	}

	protected ArrayList<PlayerController> loadBotsFromJarFile(JarFile jarFile) {
		ArrayList<PlayerController> result = new ArrayList<>();

		var entries = jarFile.entries();
		while (entries.hasMoreElements()) {
			var entry = entries.nextElement();

			var loadedClass = loadClass(entry);

			if (loadedClass != null)
				result.add(loadedClass);
		}

		return result;
	}

	public ArrayList<PlayerController> loadBotsFromPath(String path) {
		ArrayList<PlayerController> result = new ArrayList<>();

		var jarList = loadJarFilesFromPath(path);
		for (var jarFile : jarList) {
			result.addAll(loadBotsFromJarFile(jarFile));
		}

		return result;
	}
}
