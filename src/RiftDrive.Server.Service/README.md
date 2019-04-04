The Service library may only depend on the Model and Repository libraries.

Services only accept repositories, and their sole purpose is to provide the logic that can stitch multiple repositories together,
operate on multiple repositories simultaneously if the data needs requires it, and to otherwise pass the data operations through.

No app logic should be applied at this layer.  That is - the service has no idea _why_ you would do something, only _how_ it
would get done.  It should not generate any data, all data should be given to it.