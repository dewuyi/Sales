Tasks

Please find a selection of tasks the below. You do not have to complete 
them all, but I would recommend doing them in the order provided.

1. Compilation and Runtime issues

The application wont compile and also has run time errors, please try 
fix all the issues so the application can run.

2. Logical issues And Refactoring

There are some logical errors in the code, please can they be corrected. 
This is where writing tests will become very useful to validate the errors 
have been fixed and code can be refactored using SOLID.

3. Order Validation

Please follow the user story below to add the additinal functionality.

As a order entry clerk
I want to save an order with a valid order number
so that I can verify that I have entered everything correctly when saving.

Acceptance Criteria:

- Does the order number have the valid format starting with YN then 2 digits 
  followed by a dash and then another 4 digites (YN00-0000)?
- When I provide an incorretly formatted order number do I get a bad status?

4. Read an Order

Please follow the user story below to add the additinal functionality.

As a support desk assistant
I want to find an order by its order number
so that I can read it when a customer has an issue.

Acceptance Criteria:

- Does the full order (include order items) get returned when I provide a valid order number?
- Does an not found status code get returned when I provide an invalid order number?