let translations = {};

const loadTranslations = (culture) => {
    return $.ajax({
        url: `/Localization/GetTranslations?culture=${culture}`,
        type: "get",
        success: function (data) {
            translations = data;
        },
        error: function () {
            console.error("Failed to load translations");
        }
    });
};

const getTranslation = (key) => translations[key] || key;

const setupCategoryDropdown = (categories, container) => {
    container.empty();
    categories.forEach(category => {
        container.append(
            `<li><a class="dropdown-item" data-value="${category.id}">${category.translationKey ? getTranslation(category.translationKey) : category.name}</a></li>`
        );
    });

    const searchInput = $("#dropdownSearch");
    const dropdownButton = $("#dropdownMenuButtonText");
    const selectedCategory = $("#selectedCategory");
    const resetCategory = $("#resetCategory");

    searchInput.on("input", function () {
        const query = $(this).val().toLowerCase();
        container.find(".dropdown-item").each(function () {
            $(this).toggle($(this).text().toLowerCase().includes(query));
        });
    });

    container.on("click", ".dropdown-item", function (e) {
        e.preventDefault();
        dropdownButton.text($(this).text());
        selectedCategory.val($(this).data("value"));
    });

    resetCategory.on("click", function (e) {
        e.preventDefault();
        selectedCategory.val("");
        dropdownButton.text(getTranslation("ChooseCategory"));
    });
};

const loadProductCategories = () => {
    const container = $("#dropdownOptions");

    $.ajax({
        url: `/ProductCategories/GetProductCategories`,
        type: "get",
        success: function (data) {
            setupCategoryDropdown(data, container);
        },
        error: function () {
            console.error("Failed to load categories");
        }
    });
};

const initializeCategoryFormSubmission = () => {
    $("#createProductCategoryModal form").submit(function (event) {
        event.preventDefault();
        const token = $('meta[name="csrf-token"]').attr("content");

        $.ajax({
            url: $(this).attr("action"),
            type: $(this).attr("method"),
            data: $(this).serialize(),
            headers: {
                'RequestVerificationToken': token
            },
            success: function (data) {
                loadProductCategories();
                $("#createProductCategoryModal").modal("hide");
                $("#dropdownMenuButtonText").text(data.name);
                $("#selectedCategory").val(data.id);
            },
            error: function (xhr) {
                if (xhr.status === 400) {
                    const errors = xhr.responseJSON;
                    const $form = $("#createProductCategoryModal form");
                    $form.find("span.text-danger").text("");

                    $.each(errors, function (key, messages) {
                        $form.find(`[data-valmsg-for='${key}']`).text(messages.join(", "));
                    });
                } else {
                    console.error("An unexpected error occurred.");
                }
            }
        });
    });
};

// Load translations once when the document is ready
$(document).ready(function () {
    const currentCulture = $("html").attr("lang") || "en";
    loadTranslations(currentCulture);
});


// Initialize all tooltips
const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));