package BashProject;

import BashProject.bash.Bash;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class App {
	public static void main(String[] args) {
		var applicationContext = SpringApplication.run(App.class, args);

		Bash bash = (Bash) applicationContext.getBean("bash");
		bash.run();
	}
}
