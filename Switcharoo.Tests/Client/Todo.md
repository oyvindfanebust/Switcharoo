﻿- [ ] Conditionally execute an Action - switcharoo.For<FeatureA>().On(() => Console.WriteLine("active")) //.Off(() => Console.WriteLine("inactive"))
- [ ] Conditionally execute a Func<T> - var res = switcharoo.For<FeatureA, string>().On(() => "active").Off(() => "inactive")
- [ ] Conditionally return a T - var impl = switcharoo.For<FeatureA, IFoo>().On<FooActive>().Off<FooInactive>()