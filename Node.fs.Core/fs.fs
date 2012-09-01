﻿namespace Node.fs.Core.fs
open System.IO

type fs = class
   
    new() = {}

    member self.rename(oldPath, newPath, ?callback) = 
        raise (System.NotImplementedException())

    member self.renameSync(oldPath, newPath) = 
        raise (System.NotImplementedException())

    member self.truncate(fd, len, ?callback) = 
        raise (System.NotImplementedException())

    member self.truncateSync(fd, len) = 
        raise (System.NotImplementedException())

    member self.chown(path, uid, gid, ?callback) = 
        raise (System.NotImplementedException())

    member self.chownSync(path, uid, gid) = 
        raise (System.NotImplementedException())

    member self.fchown(fd, uid, gid, ?callback) = 
        raise (System.NotImplementedException())

    member self.fchownSync(fd, uid, gid) = 
        raise (System.NotImplementedException())

    member self.lchown(path, uid, gid, ?callback) = 
        raise (System.NotImplementedException())

    member self.lchownSync(path, uid, gid) = 
        raise (System.NotImplementedException())

    member self.chmod(path, mode, ?callback) = 
        raise (System.NotImplementedException())

    member self.chmodSync(path, mode) = 
        raise (System.NotImplementedException())

    member self.fchmod(fd, mode, ?callback) = 
        raise (System.NotImplementedException())

    member self.fchmodSync(fd, mode) = 
        raise (System.NotImplementedException())

    member self.lchmod(path, mode, ?callback) = 
        raise (System.NotImplementedException())

    member self.lchmodSync(path, mode) = 
        raise (System.NotImplementedException())

    member self.stat(path, ?callback) = 
        raise (System.NotImplementedException())

    member self.lstat(path, ?callback) = 
        raise (System.NotImplementedException())

    member self.fstat(fd, ?callback) = 
        raise (System.NotImplementedException())

    member self.statSync(path) = 
        raise (System.NotImplementedException())

    member self.lstatSync(path) = 
        raise (System.NotImplementedException())

    member self.fstatSync(fd) = 
        raise (System.NotImplementedException())

    member self.link(srcpath, dstpath, ?callback) = 
        raise (System.NotImplementedException())

    member self.linkSync(srcpath, dstpath) = 
        raise (System.NotImplementedException())

    member self.symlink(srcpath, dstpath, ?linkType0, ?callback) = 
        let linkType = defaultArg linkType0 "file"
        raise (System.NotImplementedException())

    member self.symlinkSync(srcpath, dstpath, ?linkType0) = 
        let linkType = defaultArg linkType0 "file"
        raise (System.NotImplementedException())

    member self.readlink(path, ?callback) = 
        raise (System.NotImplementedException())

    member self.readlinkSync(path) = 
        raise (System.NotImplementedException())

    member self.realpath(path, callback) = 
        raise (System.NotImplementedException())

    member self.realpath(path, cache, callback) = 
        raise (System.NotImplementedException())

    member self.realpathSync(path, ?cache) = 
        raise (System.NotImplementedException())

    member self.unlink(path, ?callback) = 
        raise (System.NotImplementedException())

    member self.unlinkSync(path) = 
        raise (System.NotImplementedException())

    member self.rmdir(path, ?callback) = 
        raise (System.NotImplementedException())

    member self.rmdirSync(path) = 
        raise (System.NotImplementedException())

    member self.mkdir(path, ?mode, ?callback) = 
        raise (System.NotImplementedException())

    member self.mkdirSync(path, ?mode) = 
        raise (System.NotImplementedException())

    member self.readdir(path, ?callback) = 
        raise (System.NotImplementedException())

    member self.readdirSync(path) = 
        raise (System.NotImplementedException())

    member self.close(fd, ?callback) = 
        raise (System.NotImplementedException())

    member self.closeSync(fd) = 
        raise (System.NotImplementedException())

    member self.fopen(path, flags, ?mode, ?callback) = 
        raise (System.NotImplementedException())

    member self.openSync(path:string, flags, ?mode) = 
        let mode, access = self.getModeAndAccess flags

        new FileStream(path, mode, access)

    member self.getModeAndAccess mode = 
        match mode with
        | "r" -> (FileMode.Open, FileAccess.Read)
        | "w" -> (FileMode.Open, FileAccess.Write)
        | "r+" -> (FileMode.Open, FileAccess.ReadWrite)
        | _ -> (FileMode.Open, FileAccess.Read)

  
    member self.utimes(path, atime, mtime, ?callback) = 
        raise (System.NotImplementedException())

    member self.utimesSync(path, atime, mtime) = 
        raise (System.NotImplementedException())

    member self.futimes(fd, atime, mtime, ?callback) = 
        raise (System.NotImplementedException())

    member self.futimesSync(fd, atime, mtime) = 
        raise (System.NotImplementedException())

    member self.fsync(fd, ?callback) = 
        raise (System.NotImplementedException())

    member self.fsyncSync(fd) = 
        raise (System.NotImplementedException())

    member self.write(fd, buffer, offset, length, position, ?callback) = 
        raise (System.NotImplementedException())

    member self.writeSync(fd, buffer, offset, length, position) = 
        raise (System.NotImplementedException())

    member self.read(fd, buffer, offset, length, position, ?callback) = 
        raise (System.NotImplementedException())

    member self.readSync(fd, buffer, offset, length, position) = 
        raise (System.NotImplementedException())

    member self.readFile(filename, callback) = 
         System.Threading.Tasks.Task.Factory.StartNew(fun () ->
            let bytes = self.readFileSync(filename)
            callback bytes
        )

    member self.readFile(filename, encoding, callback) = 
         System.Threading.Tasks.Task.Factory.StartNew(fun () ->
            let bytes = self.readFileSync(filename, encoding)
            callback bytes
        )

    member self.readFileSync(filename) = 
        let bytes = System.IO.File.ReadAllBytes filename
        bytes

    member self.readFileSync(filename, encoding) = 
        let enc = self.getEncoding encoding
        let data = File.ReadAllText(filename, enc)
        data
        
    member self.getEncoding enc : System.Text.Encoding = 
        match enc with
        | "utf8" -> upcast new System.Text.UTF8Encoding()
        | _ -> upcast new System.Text.ASCIIEncoding()

    member self.writeFile(filename, data, ?encoding, ?callback) = 
        raise (System.NotImplementedException())

    member self.writeFileSync(filename, data, ?encoding) = 
        raise (System.NotImplementedException())

    member self.appendFile(filename, data, ?encoding0, ?callback) = 
        let encoding = defaultArg encoding0 "utf8"
        raise (System.NotImplementedException())

    member self.appendFileSync(filename, data, ?encoding0) = 
        let encoding = defaultArg encoding0 "utf8"
        raise (System.NotImplementedException())

    member self.watchFile(filename, listener) = 
        raise (System.NotImplementedException())

    member self.watchFile(filename, options, listener) = 
        raise (System.NotImplementedException())
    member self.unwatchFile(filename, ?listener) = 
        raise (System.NotImplementedException())

    member self.watch(filename, ?options, ?listener) = 
        raise (System.NotImplementedException())

    member self.exists(path, callback) =

        System.Threading.Tasks.Task.Factory.StartNew(fun () ->
            let exists = self.existsSync path;
            callback exists
        )
        

    member self.existsSync(path) = 
        System.IO.File.Exists(path)

end
