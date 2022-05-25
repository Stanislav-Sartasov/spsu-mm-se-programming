package BashProject.tokenizer;

import BashProject.tokenizer.Lexeme.Lexeme;

import java.util.List;

public interface Tokenizer {
	List<Lexeme> tokenize(String input);
}
