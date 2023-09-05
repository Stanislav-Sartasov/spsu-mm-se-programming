package BashProject;

import BashProject.bash.Bash;
import org.springframework.boot.Banner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class App {
	public static void main(String[] args) {
		var app = new SpringApplication(App.class);
		app.setLogStartupInfo(false);
		app.setBannerMode(Banner.Mode.OFF);
		var applicationContext = app.run(args);

		Bash bash = (Bash) applicationContext.getBean("bash");
		bash.run();
	}
}
