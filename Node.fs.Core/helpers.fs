module helpers

let getEncoding enc : System.Text.Encoding = 
    match enc with
    | "utf8" -> upcast new System.Text.UTF8Encoding()
    | _ -> upcast new System.Text.ASCIIEncoding()


let getBytes(string:string, enc) =
    match enc with 
    | "utf8" -> System.Text.Encoding.UTF8.GetBytes(string)
    | "ascii" -> System.Text.Encoding.ASCII.GetBytes(string)
    | _ -> raise (System.NotImplementedException())
  