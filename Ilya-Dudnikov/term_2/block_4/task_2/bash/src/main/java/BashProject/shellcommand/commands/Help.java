package BashProject.shellcommand.commands;

import BashProject.shellcommand.Command;

import java.nio.ByteBuffer;

public class Help extends Command {
	@Override
	public ByteBuffer run(String ... args) {
		return ByteBuffer.wrap(
				("Available commands:" + System.lineSeparator()
				+ "cat [arg0] [arg1] ..." + System.lineSeparator() + "\t - prints contents of [arg0], [arg1], ..." + System.lineSeparator()
				+ "wc [arg0] [arg1] ..." + System.lineSeparator() + "\t - prints number of newlines, words and bytes in [arg0], [arg1], ..." + System.lineSeparator()
				+ "echo [arg0] [arg1] ..." + System.lineSeparator() + "\t - prints [arg0] [arg1] ..." + System.lineSeparator()
				+ "pwd" + System.lineSeparator() +"\t - prints present working directory" + System.lineSeparator()
				+ "ls [arg0] [arg1] ..." + System.lineSeparator() + "\t - lists files in [arg0] [arg1] ...; lists files in the current directory if used without arguments" + System.lineSeparator()
				+ "exit [arg]" + System.lineSeparator() + "\t - terminates the program with exit code [arg]; equivalent to `exit 0` if called without arguments" + System.lineSeparator()
				+ "It is also possible to start other apps. For example `whoami` will execute system `whoami` command" + System.lineSeparator()
				+ "Have fun :)" + System.lineSeparator()).getBytes()
		);
	}
}
