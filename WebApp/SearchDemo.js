$(function () {
    $("#q").autocomplete({
        source: "/home/suggest",
        minLength: 2,
        select: function (event, ui) {
            Search();
        }
    });

    // Execute search if user clicks enter
    $("#q").keyup(function (event) {
        if (event.keyCode == 13) {
            Search();
        }
    });

    // Execute Search if Year Drop Downs Change
    /* $("#yearMin").change(function (event) {
         Search();
     });
 
     // Execute Search if Year Drop Downs Change
     $("#yearMax").change(function (event) {
         Search();
     });*/
});

var yearMin, yearMax, makeFacet, modelFacet, milesFacet,
        isNewFacet, vehicleTypeFacet, isImportFacet,
        transmissionFacet, cylindersFacet, mpgCityFacet,
        mpgHighwayFacet, colorExteriorFacet, colorInteriorFacet,
        sizeFacet, featuresFacet, packagesFacet,
        sortType, currentPage;

$(function () {
    // Load the initial data
    currentPage = 1;
    yearMin = 2005;
    yearMax = 2016;
    makeFacet = '';
    modelFacet = '';
    milesFacet = '';
    isNewFacet = '';
    vehicleTypeFacet = '';
    isImportFacet = '';
    transmissionFacet = '';
    cylindersFacet = '';
    mpgCityFacet = '';
    mpgHighwayFacet = '';
    colorExteriorFacet = '';
    colorInteriorFacet = '';
    sizeFacet = '';
    featuresFacet = '';
    packagesFacet = '';

    document.getElementById("q").focus();
    sortType = 'miles_low';

    Search();

   
});

function Search() {
    $("#results_details_div").html("Loading...");
    var q = $("#q").val();
    // Get center of map to use to score the search results
    $.post('/home/search',
    {
        q: q,
        yearMin: yearMin,
        yearMax: yearMax,
        makeFacet: makeFacet,
        modelFacet: modelFacet,
        milesFacet: milesFacet,
        isNewFacet: isNewFacet,
        vehicleTypeFacet: vehicleTypeFacet,
        isImportFacet: isImportFacet,
        transmissionFacet: transmissionFacet,
        cylindersFacet: cylindersFacet,
        mpgCityFacet: mpgCityFacet,
        mpgHighway: mpgHighwayFacet,
        colorExteriorFacet: colorExteriorFacet,
        colorInteriorFacet: colorInteriorFacet,
        featuresFacet: featuresFacet,
        packagesFacet: packagesFacet,
        sizeFacet: sizeFacet,
        sortType: sortType,
        currentPage: currentPage
    },
    function (data) {
        UpdateMakeFacets(data.Facets.Make);
        UpdateModelFacets(data.Facets.Model);
        UpdateMilesFacets(data.Facets.Miles);
        UpdateResultsDetails(data);
        UpdatePagination(data.Count);
        UpdateFilterReset();
    });

}

function UpdateFilterReset() {
    // This allows users to remove filters
    var htmlString = '';
    if ((makeFacet != '') || (modelFacet != '') || (milesFacet != '')) {
        htmlString += '<b>Current Filters:</b><br>';
        if (makeFacet != '')
            htmlString += makeFacet + ' [<a href="javascript:void(0)" onclick="RemoveFacet(\'makeFacet\')">X</a>]<br>';
        if (modelFacet != '')
            htmlString += modelFacet + ' [<a href="javascript:void(0)" onclick="RemoveFacet(\'modelFacet\')">X</a>]<br>';
        if (milesFacet != '') {
            var lowRange = parseInt(milesFacet);
            var highRange = lowRange + 9999;
            htmlString += lowRange.toLocaleString() + ' - ' + highRange.toLocaleString() + ' [<a href="javascript:void(0)" onclick="RemoveFacet(\'milesFacet\')">X</a>]<br>';
        }
    }
    $("#filterReset").html(htmlString);
}

function RemoveFacet(facet) {
    // Remove a facet
    if (facet == "makeFacet")
        makeFacet = '';
    if (facet == "modelFacet")
        modelFacet = '';
    if (facet == "milesFacet")
        milesFacet = '';

    Search();
}

function UpdatePagination(docCount) {
    // Update the pagination
    var totalPages = Math.round(docCount / 10);
    // Set a max of 5 items and set the current page in middle of pages
    var startPage = currentPage;
    if ((startPage == 1) || (startPage == 2))
        startPage = 1;
    else
        startPage -= 2;

    var maxPage = startPage + 5;
    if (totalPages < maxPage)
        maxPage = totalPages + 1;
    var backPage = parseInt(currentPage) - 1;
    if (backPage < 1)
        backPage = 1;
    var forwardPage = parseInt(currentPage) + 1;

    var htmlString = '<li><a href="javascript:void(0)" onclick="GoToPage(\'' + backPage + '\')" class="fa fa-angle-left"></a></li>';
    for (var i = startPage; i < maxPage; i++) {
        if (i == currentPage)
            htmlString += '<li  class="active"><a href="#">' + i + '</a></li>';
        else
            htmlString += '<li><a href="javascript:void(0)" onclick="GoToPage(\'' + parseInt(i) + '\')">' + i + '</a></li>';
    }

    htmlString += '<li><a href="javascript:void(0)" onclick="GoToPage(\'' + forwardPage + '\')" class="fa fa-angle-right"></a></li>';
    $("#pagination").html(htmlString);
    $("#paginationFooter").html(htmlString);


}

function GoToPage(page) {
    currentPage = page;
    Search();
}

function UpdateMakeFacets(data) {
    var facetResultsHTML = '';
    for (var i = 0; i < data.length; i++) {
        facetResultsHTML += '<li><a href="javascript:void(0)" onclick="ChooseMakeFacet(\'' + data[i].Value + '\');">' + data[i].Value + ' (' + data[i].Count + ')</span></a></li>';
    }

    $("#make_facets").html(facetResultsHTML);
}

function ChooseMakeFacet(facet) {
    makeFacet = facet;
    Search();
}

function UpdateModelFacets(data) {
    var facetResultsHTML = '';
    for (var i = 0; i < data.length; i++) {
        facetResultsHTML += '<li><a href="javascript:void(0)" onclick="ChooseModelFacet(\'' + data[i].Value + '\');">' + data[i].Value + ' (' + data[i].Count + ')</span></a></li>';
    }
    $("#model_facets").html(facetResultsHTML);
}

function ChooseModelFacet(facet) {
    modelFacet = facet;
    Search();
}

function UpdateMilesFacets(data) {
    var facetResultsHTML = '';
    for (var i = 0; i < data.length; i++) {
        var lowRange = parseInt(data[i].Value);
        var highRange = lowRange + 9999;

        facetResultsHTML += '<li><a href="javascript:void(0)" onclick="ChooseMilesFacet(\'' + data[i].Value + '\');">' + lowRange.toLocaleString() + ' - ' + highRange.toLocaleString() + ' (' + data[i].Count + ')</span></a></li>';
    }

    $("#miles_facets").html(facetResultsHTML);
}

function ChooseMilesFacet(facet) {
    milesFacet = facet;
    Search();
}

function setSortType() {
    sortType = $("#cmbSortType").val();
    Search();
}


function UpdateResultsDetails(data) {
    var resultsHTML = '';

    $("#results-count").html(data.Count);

    for (var i = 0; i < data.Results.length; i++) {

        resultsHTML += '<div class="well col-md-9">';

        resultsHTML += '<h6>Stock Number: ' + data.Results[i].Document.StockNumber + '</h6>'
        resultsHTML += '<ul>';
        resultsHTML += '<li>Year: ' + data.Results[i].Document.Year + '</li>';
        resultsHTML += '<li>Make: ' + data.Results[i].Document.Make + '</li>';
        resultsHTML += '<li>Model: ' + data.Results[i].Document.Model + '</li>';
        resultsHTML += '<li>Miles: ' + data.Results[i].Document.Miles + '</li>';
        resultsHTML += '</ul>';

        resultsHTML += '</div>';

    }

    $("#results_details_div").html(jobDetailsHTML);
}