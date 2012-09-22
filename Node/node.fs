module node   
   
type Encoding = Utf8 | Ascii

let getEncoding enc : System.Text.Encoding = 
    match enc with
    | Utf8 -> upcast new System.Text.UTF8Encoding()
    | Ascii -> upcast new System.Text.ASCIIEncoding()


let getBytes(string:string, enc) =
    match enc with 
    | Utf8 -> System.Text.Encoding.UTF8.GetBytes(string)
    | Ascii -> System.Text.Encoding.ASCII.GetBytes(string)


let proc = // NAMING: should be process
    new node_process.proc()
        
let _cache = new System.Collections.Generic.Dictionary<System.Type, System.Object>()

let require<'T when 'T:(new : unit -> 'T)> =

    match _cache.TryGetValue(typeof<'T>) with
    | true, tempInstance ->
        tempInstance :?> 'T
    | false, _ ->
        let tempInstance = new 'T()
        _cache.Add(typeof<'T>, tempInstance)
        tempInstance

 

let eventsForNextTick = new System.Collections.Generic.List<unit->unit>()

let nextTick(x) = 
    eventsForNextTick.Add(x)
           
let tick () = 
    System.Threading.Thread.Sleep(1)

    let eventsToFire =  new System.Collections.Generic.List<unit->unit>(eventsForNextTick)
    eventsForNextTick.Clear()

    if eventsToFire.Count > 0 then
        eventsToFire |> Seq.cast |> Seq.iter(fun f -> f())
        
// Starts the event loop
let start func =
    
    Node.emitter.EmitterMethods.tickMethod <- fun x -> nextTick(x)
    func()

    while true do
        tick()

        
