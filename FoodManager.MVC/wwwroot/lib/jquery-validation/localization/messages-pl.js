/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: PL
 */
$.extend($.validator.messages, {
	required: "To pole jest wymagane.",
	remote: "Proszę o wypełnienie tego pola.",
	email: "Proszę o wprowadzenie prawidłowego adresu email.",
	url: "Proszę o wprowadzenie prawidłowego URL.",
	date: "Proszę o wprowadzenie prawidłowej daty.",
	dateISO: "Proszę o wprowadzenie prawidłowej daty (ISO).",
	number: "Proszę o wprowadzenie prawidłowej liczby.",
	digits: "Proszę o wprowadzenie samych cyfr.",
	equalTo: "Proszę o wprowadzenie tej samej wartości ponownie.",
    minlength: $.validator.format("Proszę o wprowadzenie przynajmniej {0} znaków."),
	maxlength: $.validator.format("Proszę o wprowadzenie nie więcej niż {0} znaków."),
    min: $.validator.format("Proszę o wprowadzenie wartości większej bądź równej {0}."),
	max: $.validator.format("Proszę o wprowadzenie wartości mniejszej bądź równej {0}."),
	range: $.validator.format("Proszę o wprowadzenie wartości z przedziału od {0} do {1}."),
    rangelength: $.validator.format("Proszę o wprowadzenie wartości o długości od {0} do {1} znaków."),
    step: $.validator.format("Proszę o wprowadzenie wielokrotności {0}.")
} );
