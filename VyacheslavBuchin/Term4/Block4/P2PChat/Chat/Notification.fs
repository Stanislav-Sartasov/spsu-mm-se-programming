namespace Chat

type Notification =
    | Message of string * string
    | Connect of string
    | Disconnect of string
