The Service library may only depend on the Model, Provider and Repository libraries.

Services only accept repositories, and their sole purpose is to provide the logic that can stitch multiple repositories together,
operate on multiple repositories simultaneously if the data needs requires it, and to otherwise pass the data operations through.
