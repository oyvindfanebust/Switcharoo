﻿- [ ] Conditionally execute an Action - switcharoo.For<FeatureA>(() => Console.WriteLine("active"))
- [ ] Conditionally execute a Func<T> - string res = switcharoo.For<FeatureA, string>(() => "active", () => "inactive")
- [ ] Conditionally return a T - var impl = switcharoo.For<FeatureA, IFoo>().On<FooActive>().Off<FooInactive>()