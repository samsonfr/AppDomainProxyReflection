This project is licenced under Microsoft Public License (MS-PL).

It is only a proof of concept to demonstrate a technique of loading plugins into a temporary AppDomain in order to use them without locking them on disk.

This version works without having to define an Interface. Reflection classes proxies have been defined in order to use Reflection without loading the called assembly in the main AppDomain.

Frederick Samson
http://samsonfr.wordpress.com


