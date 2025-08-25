
S
Single Responsibility Principle (SRP):

"A class should have only one reason to change."

In simpler terms, a class should only have one job or responsibility. If a class has more than one reason to change, it has more than one responsibility and should be refactored.


Why is SRP Important?
	Better Maintainability: Classes with a single responsibility are easier to understand and modify.
	Increased Reusability: Classes can be reused in different contexts without dragging along unrelated functionality.
	Improved Testability: Smaller, focused classes are easier to test independently.
	Enhanced Readability: Code is clearer and more understandable when each class has a single purpose.
	Reduced Complexity: Simplifies the design by breaking down complex systems into manageable parts.
	Parallel work: Teams can modify different responsibilities without stepping on each other.
	Lower risk: A change in one responsibility is less likely to break something unrelated.

Violating SRP leads to?
	Mixed concerns: Business rules, formatting, I/O, and orchestration sit in the same class.
	Unclear responsibilities: It becomes difficult to understand what the class is supposed to do.
	Unpredictable changes: Changing one part of the class may inadvertently affect other parts.
	Tightly coupled code: Changing one responsibility may affect others.
	Harder maintenance: Understanding or modifying the code becomes difficult.
	Low reusability: You can’t reuse the class without dragging along unrelated responsibilities.

How to Apply SRP?
	Identify Responsibilities: Break down the class into distinct responsibilities.
	Create Separate Classes: Each responsibility should be encapsulated in its own class.
	Use Interfaces: Define interfaces for each responsibility to promote loose coupling.
	Refactor Regularly: Regularly review and refactor your code to ensure SRP is maintained.
	Test Independently: Ensure that each class can be tested independently of others.
	Document Responsibilities: Clearly document what each class is responsible for to avoid confusion.
	Keep Classes Small: Aim for small, focused classes that do one thing well.
	Avoid God Classes: Don’t let a single class grow too large or take on too many responsibilities.



The implementation of Good Design, in this project, adheres to the Single Responsibility Principle (SRP) along with other Design Principles and Design Patterns:
	
	1) Dependency Injection (DI)
		•	The constructor of InvoiceService takes interfaces (IInvoiceRepository, IInvoiceFormatter, IEmailSender) as parameters.
		•	This allows for loose coupling and easier testing/mocking.
		•	Pattern: Dependency Injection

	2) Interface Segregation Principle (ISP)
		•	The use of interfaces (IInvoiceRepository, IInvoiceFormatter, IEmailSender) abstracts the implementation details.
		•	This supports the Interface Segregation Principle and promotes abstraction.
		•	Principle: Interface Segregation Principle (from SOLID)

	3) Strategy Pattern
		•	The formatting and email sending behaviors are encapsulated in separate classes implementing interfaces.
		•	This allows changing the formatting or email sending strategy at runtime by injecting different implementations.
		•	Pattern: Strategy Pattern

	4) Repository Pattern
		•	IInvoiceRepository abstracts the data persistence logic.
		•	This separates the domain/business logic from data access logic.
		•	Pattern: Repository Pattern
		
	5) Single Responsibility Principle (SRP)
		•	InvoiceService acts as an orchestration/application service, delegating responsibilities to other classes:
		•	Formatting: IInvoiceFormatter
		•	Persistence: IInvoiceRepository
		•	Notification: IEmailSender
		•	Each class/interface has a clear, single responsibility.
		•	Principle: Single Responsibility Principle (from SOLID)


