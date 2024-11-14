const loadProductCategoriesFilters = () => {
    const container = $("#categoryFilterList");

    $.ajax({
        url: `/ProductCategories/GetProductCategories`,
        type: "get",
        success: function (data) {
            setupCategoryFilter(data, container);
        },
        error: function () {
            console.error("Failed to load services");
        }
    });
};

const setupCategoryFilter = (categories, container) => {
    container.empty();

    const translatedCategories = categories.map(category => ({
        translatedName: category.translationKey ? getTranslation(category.translationKey).toLocaleLowerCase() : category.name,
        value: category.id
    }));

    const uncategorizedCategory = { translatedName: getTranslation("Uncategorized").toLocaleLowerCase(), value: -1 };

    translatedCategories.push(uncategorizedCategory);
    translatedCategories.sort((a, b) => a.translatedName.localeCompare(b.translatedName));

    translatedCategories.forEach(category => {
        container.append(
            `
            <label class="list-group-item d-flex justify-content-between align-items-center">
            <span>${category.translatedName}</span>
            <input class="form-check-input" type="checkbox" name="CategoryIds" value="${category.value}" ${selectedCategoryIds?.includes(String(category.value)) ? "checked" : ""}/>
            </label>
            `
        );
    });
};

$(document).ready(function () {
    loadProductCategoriesFilters();
});