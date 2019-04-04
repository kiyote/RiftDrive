The Repository library may only depend on the Model and Shared library.

The repository layer acts as the translation between the Model objects as used by the application and the model required by the
chosen persistence layer.  Its API may only accept or return classes from the Model layer.  Internally it will have to translate
whatever classes it may need, but this must not be visible to the consumer.

Repositories should only implement CRUD operations.  Each operation being a semantic operation on the data without any logic applied.
The repository has no idea why something may be requested or written, it only concerns itself with how to read or write the data
in an efficient manner.

The operation offered should be high-level enough that it can be encapsulated within the persistence layer.  The next higher layer
should not have to implement any data logic unless the operation is extremely expensive or challenging within the persistence layer.
