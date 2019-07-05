# Automatic Truth Table

This class library was generated as an extra credit project for a Discrete Mathematics course. The console interface will ask a user for a statement of operations on propositions. The `.Write()` method of the `TruthTable` class will then automatically write out a truth table.

Note that **_using parenthesis is extremely important for the program to compute the correct answer_**. For example, `~(p || q) == ~p && ~q` will register to be always false, while `(~(p || q)) == (~p && ~q)` will register to be a tautology. This may or may not be improved upon in the near future, but (regardless) it will always be safer to over-use parenthesis!

#### Potential Future Improvements:
- More automatic knowledge of order of operations (i.e., require less parenthesis from user)
- More flexibility with proposition names
- Verify format of user input
- Create web interface for demo using github pages
- Indicate when statement is a tautology
