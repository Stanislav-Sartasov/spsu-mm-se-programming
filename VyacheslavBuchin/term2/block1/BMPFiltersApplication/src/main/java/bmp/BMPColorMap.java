package bmp;

public record BMPColorMap(BMPColor[][] pixels) {
    public BMPColorMap {
        if (pixels.length == 0)
            throw new IllegalArgumentException("Given array must not be empty");
        for (int i = 1; i < pixels.length; i++) {
            if (pixels[i].length != pixels[i - 1].length)
                throw new IllegalArgumentException("Given array must be rectangular");
        }
    }

    public int width() {
        return pixels[0].length;
    }

    public int height() {
        return pixels.length;
    }

    public BMPColor get(int i, int j) {
        return pixels[i][j];
    }

    public BMPColor set(int i, int j, BMPColor newValue) {
        var oldValue = pixels[i][j];
        pixels[i][j] = newValue;

        return oldValue;
    }

    public boolean exists(int i, int j) {
        return 0 <= i && i < height() &&
                0 <= j && j < width();
    }

}
