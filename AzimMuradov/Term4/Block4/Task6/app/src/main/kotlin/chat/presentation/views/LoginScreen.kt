/*
 * Copyright 2021-2023 Azim Muradov
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package chat.presentation.views

import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp


@Composable
fun LoginScreen(onLogin: (String) -> Unit) {
    Column(
        modifier = Modifier.fillMaxSize().padding(128.dp),
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Spacer(Modifier.weight(1f))

        var value by remember { mutableStateOf("") }

        OutlinedTextField(
            value = value,
            onValueChange = { if (it.length <= 16) value = it },
            modifier = Modifier.widthIn(max = 300.dp).fillMaxWidth().height(64.dp),
            placeholder = { Text(text = "choose a nickname...") },
            singleLine = true,
            colors = TextFieldDefaults.outlinedTextFieldColors(
                backgroundColor = MaterialTheme.colors.surface
            )
        )

        Spacer(Modifier.size(24.dp))

        Button(
            onClick = { onLogin(value) },
            modifier = Modifier.widthIn(max = 300.dp).fillMaxWidth().height(64.dp)
        ) {
            Text(text = "JOIN")
        }

        Spacer(Modifier.weight(1f))
    }
}
