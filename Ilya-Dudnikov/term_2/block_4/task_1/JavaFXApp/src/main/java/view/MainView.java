	private final String RESOURCES_PATH = "src/main/resources/";
	private final String HOVERED_REFRESH_BUTTON_STYLE = "" +
			"-fx-background-color: rgba(43, 43, 43, .3);" +
			"-fx-background-radius: 50%";
	private final String REFRESH_BUTTON_STYLE = "" +
			"-fx-background-color: transparent;" +
			"-fx-background-radius: 50%";
	private final String ON_CLICK_REFRESH_BUTTON = "" +
			"-fx-background-color: rgba(43, 43, 43, .5);" +
			"-fx-background-radius: 50%";
		Button refreshButton = new Button();
		refreshButton.setGraphic(new ImageView("file:" + RESOURCES_PATH + "refresh.png"));
		refreshButton.setContentDisplay(ContentDisplay.GRAPHIC_ONLY);
		refreshButton.setStyle(REFRESH_BUTTON_STYLE);
		refreshButton.setOnMouseEntered(event -> refreshButton.setStyle(HOVERED_REFRESH_BUTTON_STYLE));
		refreshButton.setOnMouseExited(event -> refreshButton.setStyle(REFRESH_BUTTON_STYLE));
		refreshButton.setOnMousePressed(event -> refreshButton.setStyle(ON_CLICK_REFRESH_BUTTON));
		refreshButton.setOnMouseReleased(event -> refreshButton.setStyle(HOVERED_REFRESH_BUTTON_STYLE));
		setRight(refreshButton);
