module helpers

type Encoding = Utf8 | Ascii

let getEncoding enc : System.Text.Encoding = 
    match enc with
    | Utf8 -> upcast new System.Text.UTF8Encoding()
    | Ascii -> upcast new System.Text.ASCIIEncoding()


let getBytes(string:string, enc) =
    match enc with 
    | Utf8 -> System.Text.Encoding.UTF8.GetBytes(string)
    | Ascii -> System.Text.Encoding.ASCII.GetBytes(string)
