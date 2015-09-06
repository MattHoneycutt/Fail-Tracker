**********WARNING********
If you are upgrading from a previous version of Heroic.Web.IoC, you MAY need to update your existing StructureMapConfig!  

If your StructureMapConfig file contains any references to ObjectFactory, please replace them with IoC.Container instead, like so:

//Old code
ObjectFactory.Configure(cfg => 
{
	//Stuff
});

//New code:
IoC.Container.Configure(cfg => 
{
	//Stuff
});

Any other references you have created to ObjectFactory should also be replaced with IoC.Container. 