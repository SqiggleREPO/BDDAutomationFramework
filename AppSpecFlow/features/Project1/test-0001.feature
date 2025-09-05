Feature: test-0001 Login Functionality
# Command: dotnet test --filter:"TestCategory=test-0001" --logger "trx;logfilename=test-0001.trx"

@test-0001
Scenario Outline: test-0001-testing login with valid credentials
    Given I am testing "Project1"
    And I have navigated to the application
    Then page "Swag Labs" is displayed

    When I enter "standard_user" in textbox "Username"
    Then textbox "Username" contains "standard_user"

    When I enter "secret_sauce" in textbox "Password"
    Then textbox "Password" contains "secret_sauce"

    When I click button "Login"
    #Then page "Products" is displayed
    ##Then I should see "dashboard" element
    ##And I should see text "Welcome"
