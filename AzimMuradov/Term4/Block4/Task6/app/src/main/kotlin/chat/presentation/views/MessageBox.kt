package chat.presentation.views

import androidx.compose.foundation.layout.*
import androidx.compose.material.Card
import androidx.compose.material.MaterialTheme
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import chat.data.models.MessageData
import chat.presentation.state.User
import java.time.Instant
import java.time.ZoneId
import java.time.format.DateTimeFormatter
import java.time.format.FormatStyle
import java.util.*


@Composable
fun MessageBox(user: User, message: MessageData) {
    val sender = (user as? User.NotMe)?.name?.name
    val (text, epoch) = message
    val instant = Instant.ofEpochSecond(epoch)
    val formatted = DateTimeFormatter
        .ofLocalizedTime(FormatStyle.MEDIUM)
        .withLocale(Locale.US)
        .withZone(ZoneId.systemDefault())
        .format(instant)

    Row {
        if (sender == null) Spacer(Modifier.weight(1f))

        Card(
            modifier = Modifier.widthIn(min = 128.dp),
            shape = MaterialTheme.shapes.medium,
            backgroundColor = MaterialTheme.colors.surface,
            contentColor = MaterialTheme.colors.onSurface
        ) {
            Column(
                modifier = Modifier.padding(8.dp),
                horizontalAlignment = if (sender == null) Alignment.End else Alignment.Start
            ) {
                if (sender != null) {
                    Text(
                        text = sender,
                        color = MaterialTheme.colors.primary,
                        fontWeight = FontWeight.Bold,
                        style = MaterialTheme.typography.caption
                    )
                    Spacer(Modifier.size(4.dp))
                }
                Text(text, style = MaterialTheme.typography.body1)
                Spacer(Modifier.size(4.dp))
                Text(formatted, style = MaterialTheme.typography.overline)
            }
        }

        if (sender != null) Spacer(Modifier.weight(1f))
    }
}
