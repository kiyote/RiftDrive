The model library may only depend on the Shared library.

Model objects are to be simple POCOs.  If the object is ever to be part of a collection, it must implement IEquatable<T> in order
to ensure the collection operations work as expected.

The classes should not perform ANY logic whatsoever.  The objects must be composed to types that are innately serializable.  For example,
we can use Id<T> because it supplies its own serializer.

Model objects must be immutable and utilize JsonConstructor to avoid property setting.