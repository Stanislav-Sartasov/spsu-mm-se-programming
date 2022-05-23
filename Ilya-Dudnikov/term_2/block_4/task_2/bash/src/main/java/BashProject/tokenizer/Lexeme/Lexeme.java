package BashProject.tokenizer.Lexeme;

import org.springframework.stereotype.Component;

@Component
public class Lexeme {
	private LexemeType type;
	private String value;

	public Lexeme(LexemeType type, String value) {
		this.type = type;
		this.value = value;
	}

	public LexemeType getType() {
		return this.type;
	}

	public String getValue() {
		return this.value;
	}

	@Override
	public boolean equals(Object o) {
		if (!(o instanceof Lexeme))
			return false;
		Lexeme other = (Lexeme) o;
		return this.type == other.type && this.value.equals(other.value);
	}
}
