The classes in here represent client-specific views of regular objects.

For example, the server has a notion of a "Player", but not all information in that class is 
suitable for transmission to the client.  So, a "ClientPlayer" projection class is created
for sharing between the client and server.
