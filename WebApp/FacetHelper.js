function ChooseFacet(facetName, selectedValue)
{
    var current = $('#currentFacets').val();
    current += '|' + facetName + ':' + selectedValue;
    $('#currentFacets').val(current);
    submitForm();
}

function RemoveFacet(facetName, facetValue)
{
    $('#facettoremove').val(facetName + ':' + facetValue + '|');
    submitForm();
}

function submitForm()
{
    $('#searchform').submit();
}
