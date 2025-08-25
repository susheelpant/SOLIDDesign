# SOLIDDesign
C# code samples for SOLID Design principles

SOLID stands for

S
Single Responsibility Principle (SRP):

"A class should have only one reason to change."


O
Open/Closed Principle (OCP): 

Software entities (classes, modules, functions) should be open for extension but closed for modification.

That means:

Open for extension ? You can add new functionality without touching existing, tested code.

Closed for modification ? You don’t have to rewrite or alter existing code to support new requirements.

This principle is about future-proofing your code. It’s a guardrail against the “ripple effect” — where a small change in one place breaks things in unexpected places. You design around stable abstractions so that when a new requirement arrives, you add new code (new classes/strategies/plugins) rather than edit existing, tested code.

Why is OCP Important?
	You add new behavior by adding new classes, not changing old ones.
	You leverage abstraction and polymorphism to make the system adaptable.
	You reduce regression risk and improve maintainability.
	
Violating OCP leads to?	
	Adding a new feature means editing old code, risking bugs.
	You end up with giant if/else or switch statements that grow endlessly.
	Testing becomes harder because every change requires retesting old logic.

