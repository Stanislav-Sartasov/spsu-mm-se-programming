import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import report.WeatherReport
import java.math.RoundingMode
import java.text.NumberFormat
import java.util.*

@Composable
fun WeatherReportView(source: String, report: WeatherReport) {
	Column(
		modifier = Modifier
			.height(250.dp).width(250.dp)
			.wrapContentSize(Alignment.TopStart)
			.padding(10.dp),
		Arrangement.spacedBy(5.dp)
	) {
		Text(source, modifier = Modifier.fillMaxWidth().padding(5.dp).align(Alignment.CenterHorizontally).background(Color.LightGray), textAlign = TextAlign.Center)
		"Temperature".rowViewWith("${report.temperature?.celsius.view("°C")} (${report.temperature?.fahrenheit.view("°F")})")
		"Cloudiness".rowViewWith(report.cloudiness?.percent.view("%"))
		"Humidity".rowViewWith(report.humidity?.percent.view("%"))
		"Precipitation".rowViewWith(report.precipitation?.mmPerHour.view("mm/h"))
		"Wind Direction".rowViewWith(report.windDirection?.degrees.view("°"))
		"Wind Velocity".rowViewWith(report.windVelocity?.metersPerSecond.view("m/s"))
	}
}

private fun Number?.view(measurementUnits: String): String {
	if (this == null)
		return "no data"
	val numberFormatter = NumberFormat.getNumberInstance(Locale.ENGLISH)
	numberFormatter.roundingMode = RoundingMode.HALF_UP
	numberFormatter.maximumFractionDigits = 1
	val result = numberFormatter.format(this)
	return "$result$measurementUnits"
}

@Composable
private fun String.rowViewWith(value: String) {
	val quantity = this
	Row(
		modifier = Modifier.fillMaxWidth(),
		horizontalArrangement = Arrangement.SpaceBetween
	) {
		Text("$quantity:")
		Text(value)
	}
}