package BashProject.interpreter;

import BashProject.tokenizer.Lexeme.Lexeme;

import java.util.List;

public interface Interpreter {
	void interpret(List<Lexeme> tokens);
}
