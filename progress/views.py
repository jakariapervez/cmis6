from django.shortcuts import render, get_object_or_404, redirect
from django.http import HttpResponse,JsonResponse
from .models import DPP_Intervention, Haor, Contract_Intervention
from django.contrib.auth.decorators import login_required
#from django.contrib.auth.models import
# Create your views here.
from .models import Contract
from wldata import views as wl_imported_view
@login_required
def siteHome(request):
    user=request.user
    print(user.profile.role.role_name)
    if user.profile.role.role_name =="XEN":
        contracts=Contract.objects.filter(xen_id=user.id)
        return render(request,'index/xen_index.html',{'contracts': contracts})
    elif(user.profile.role.role_name=="SP_ADMIN"):
        contracts = Contract.objects.all()
        return redirect('dashboard')
        #return render(request, 'index/admin_index.html',{'contracts': contracts})
    elif (user.profile.role.role_name == "FIELD_ENGG_CONSL"):
        contracts = Contract.objects.filter(fse_id=user.id)
        return render(request, 'index/fse_index.html',{'contracts': contracts})
    elif (user.profile.role.role_name == "CSE_CONSL"):
        contracts = Contract.objects.filter(cse_id=user.id)
        return render(request, 'index/Cse_index.html',{'contracts': contracts})
    elif(user.profile.role.role_name == "HYDROLOGY"):
        return redirect(wl_imported_view.wl_index)
        wl_imported_view

    else:
        return HttpResponse("This Page is under Construction")
def progressHome(request):
    return HttpResponse("This Page is under Construction")


"""Pagination related Imports """
from django.core.paginator import Paginator, Page, PageNotAnInteger, EmptyPage

@login_required
def list_DPP_Intervention(request):
    haors = Haor.objects.all()
    ivts = DPP_Intervention.objects.all().order_by('worktype_id','worktype_id','start_chainage')
    return render(request, 'progress/edit_dpp_intervention2.html', {'ivts': ivts, 'haors': haors})
@login_required
#from .auxilary_query import StrucuresHasNocontract
#from .models import Intervention_Status
def list_DPP_Intervention_sort_by_haor(request, pk):
    print(pk)
    print(request)
    table = (request.GET.get('table'))
    #myivts=Intervention_Status.objects.filter(contract_status="HAVE_NO_CONTRACT")

    ivts = DPP_Intervention.objects.filter(haor_id=pk,contract_status ="HAVE_NO_CONTRACT").order_by('worktype_id','start_chainage' )
    #for ivt in ivts:
        #print(ivt.Intervention_Status)


    data = dict()
    if table == 'contract':
        data['html_ivt_list'] = render_to_string('progress/includes/partial_contract_ivt_list.html', {
            'ivts': ivts
        })
    else:
        data['html_ivt_list'] = render_to_string('progress/includes/partial_ivt_list.html', {
            'ivts': ivts
        })
    return JsonResponse(data)

@login_required
def list_DPP_Intervention_sort_by_haor_all(request):
    table = (request.GET.get('table'))
    ivts = DPP_Intervention.objects.all()
    data = dict()
    if table == 'contract':
        data['html_ivt_list'] = render_to_string('progress/includes/partial_contract_ivt_list.html', {
            'ivts': ivts
        })
    else:
        data['html_ivt_list'] = render_to_string('progress/includes/partial_ivt_list.html', {
            'ivts': ivts
        })
    return JsonResponse(data)

@login_required
def edit_DPP_Intervention(request):
    ivt_list = DPP_Intervention.objects.all().order_by('haor_id')
    page = request.GET.get('page', 1)
    paginator = Paginator(ivt_list, 15)
    try:
        ivts = paginator.page(page)
    except PageNotAnInteger:
        ivts = paginator.page(1)
    except EmptyPage:
        ivts = paginator.page(paginator.num_pages)
    return render(request, 'progress/edit_dpp_intervention2.html', {'ivts': ivts})
    # return HttpResponse("This Page is under Construction")


"""Import Related to Ajax for DPP_Intervention_Create """

from django.template.loader import render_to_string
from .forms import DPP_intervention_form

@login_required
def save_ivt_form(request, form, template_name):
    data = dict()
    if request.method == 'POST':
        if form.is_valid():
            form.save()
            data['form_is_valid'] = True
            ivts = DPP_Intervention.objects.all()
            data['html_ivt_list'] = render_to_string('progress/includes/partial_ivt_list.html', {
                'ivts': ivts
            })
        else:
            data['form_is_valid'] = False
    context = {'form': form}
    data['html_form'] = render_to_string(template_name, context, request=request)
    return JsonResponse(data)

@login_required
def create_DPP_Intervention(request):
    if request.method == 'POST':
        form = DPP_intervention_form(request.POST)
    else:
        form = DPP_intervention_form()
    return save_ivt_form(request, form, 'progress/includes/partial_ivt_create.html')

@login_required
def update_DPP_Intervention(request, pk):
    ivt = get_object_or_404(DPP_Intervention, pk=pk)
    print(ivt)

    if request.method == 'POST':
        form = DPP_intervention_form(request.POST, instance=ivt)
    else:
        form = DPP_intervention_form(instance=ivt)
    return save_ivt_form(request, form, 'progress/includes/partial_ivt_update.html')

@login_required
def delete_DPP_Intervention2(request, pk):
    ivt = get_object_or_404(DPP_Intervention, pk=pk)
    print(ivt)
    data = dict()
    if request.method == 'POST':

        ivt.delete()
        data['form_is_valid'] = True
        ivts = DPP_Intervention.objects.all()
        data['html_ivt_list'] = render_to_string('progress/includes/partial_ivt_list.html', {
            'ivts': ivts
        })
    else:
        context = {'ivt': ivt}
        data['html_form'] = render_to_string('progress/includes/partial_ivt_delete.html', context, request=request)
    return JsonResponse(data)

@login_required
def delete_DPP_Intervention(request, pk):
    ivt = get_object_or_404(DPP_Intervention, pk=pk)
    data = dict()
    if request.method == 'POST':
        ivt.delete()
        data['form_is_valid'] = True
        ivts = DPP_Intervention.objects.all()
        data['html_ivt_list'] = render_to_string('progress/includes/partial_ivt_list.html', {
            'ivts': ivts
        })
        print(data['html_ivt_list'])
    else:
        context = {'ivt': ivt}
        data['html_form'] = render_to_string('progress/includes/partial_ivt_delete.html', context, request=request)
    return JsonResponse(data)


from .forms import DocumentForm
from .models import Document

@login_required
def model_form_upload2(request):
    if request.method == 'POST':
        form = DocumentForm(request.POST, request.FILES)
        if form.is_valid():
            form.save()
            return redirect('home2')
    else:
        form = DocumentForm()
    return render(request, 'progress/includes/model_form_upload.html', {
        'form': form
    })

@login_required
def model_form_upload(request):
    if request.method == 'POST':
        form = DocumentForm(request.POST, request.FILES)
        if form.is_valid():
            document = Document()
            document.uploaded_by = request.user
            document.document = form.cleaned_data['document']

            document.save()

            # form.save()
            return redirect('home2')
    else:
        form = DocumentForm()
    return render(request, 'progress/includes/model_form_upload.html', {
        'form': form
    })

from .forms import ConstructionImageForm
from .models import ConstructionImage
@login_required
def construction_image_upload(request):
    if request.method == 'POST':
        form = ConstructionImageForm(request.POST, request.FILES)
        if form.is_valid():
            image = ConstructionImage()
            image.uploaded_by = request.user
            image.image = form.cleaned_data['image']
            image.description=form.cleaned_data['description']
            image.structure_id=form.cleaned_data['structure_id']
            image.save()

            # form.save()
            return redirect('home2')
    else:
        form = ConstructionImageForm()
    return render(request, 'progress/includes/construction_image/construction_image_upload_form.html', {
        'form': form
    })
@login_required
def construction_image_upload_by_contract(request,pk):
    civts = Contract_Intervention.objects.filter(contract_id=pk)
    print(civts)
    if request.method == 'POST':
        form = ConstructionImageForm(request.POST, request.FILES)
        if form.is_valid():
            image = ConstructionImage()
            image.uploaded_by = request.user
            image.image = form.cleaned_data['image']
            image.description=form.cleaned_data['description']
            image.structure_id=form.cleaned_data['structure_id']
            image.save()

            # form.save()
            return redirect('home2')
    else:
        form = ConstructionImageForm(initial={'structure_id':civts})
        #form.fields["structure_id"].initial = civts
        #initial = {'subject': 'I love your site!'}

    return render(request, 'progress/includes/construction_image/construction_image_upload_form.html', {
        'form': form
    })
@login_required
def home(request):
    """
    documents = Document.objects.all()
     """
    documents=dict()
    return render(request, 'progress/includes/home.html', {'documents': documents})

    #return HttpResponse("The Page is Under Construction")



@login_required
def home2(request):
    images =ConstructionImage.objects.all()
    firstimage=ConstructionImage.objects.first()
    lastimage=ConstructionImage.objects.last()
    civts=Contract_Intervention.objects.all()
    print(firstimage)
    print(lastimage)
    context ={'images': images ,'firstimage':firstimage,'lastimage':lastimage,'civts':civts}

    return render(request, 'progress/includes/home2.html', context)
@login_required
def home2_update_ajax(request,pk):
    data=dict()
    images=ConstructionImage.objects.filter(structure_id=pk)
    firstimage =images.first()
    lastimage =images.last()
    context={'images':images,'firstimage':firstimage,'lastimage':lastimage}
    data['image_items'] = render_to_string('progress/includes/construction_image/construction_image_table_body.html',context)
    data['image_carousel'] = render_to_string('progress/includes/construction_image/Carousel.html',context)
    return JsonResponse(data)
@login_required
def add_contract_intervention(request):
    haors = Haor.objects.all()
    context = {'haors': haors}
    return render(request, 'progress/edit_drop_contract_intervention.html', context)


""" Contract Intervention Sort"""

@login_required
def list_contract_Intervention_sort_by_haor(request, pk):
    print(pk)
    ivts = DPP_Intervention.objects.filter(haor_id=pk)
    data = dict()
    data['html_ivt_list'] = render_to_string('progress/includes/partial_ivt_list.html', {
        'ivts': ivts
    })

    return JsonResponse(data)

@login_required
def list_contract_Intervention_sort_by_haor_all(request):
    ivts = DPP_Intervention.objects.all()
    data = dict()
    data['html_ivt_list'] = render_to_string('progress/includes/partial_ivt_list.html', {
        'ivts': ivts
    })

    return JsonResponse(data)


from .forms import ContractAddForm

@login_required
def update_contract_Intervention(request, pk):
    ivt = get_object_or_404(DPP_Intervention, pk=pk)
    print(ivt)

    if request.method == 'POST':
        form = ContractAddForm(request.POST, instance=ivt)
    else:
        form = ContractAddForm()
    return save_ivt_form(request, form, 'progress/includes/partial_ivt_update.html')

@login_required
def save_contract_form(request, form, template_name):
    data = dict()
    if request.method == 'POST':
        if form.is_valid():
            form.save()
            data['form_is_valid'] = True
            # contract_id= form.fields["contract_id"]
            contract_id = form.cleaned_data.get('contract_id')
            print(contract_id)
            # ivts = get_object_or_404(Contract_Intervention,=contract_id)
            ivts = Contract_Intervention.objects.filter(contract_id=contract_id)
            print(ivts)
            data['html_ivt_list'] = render_to_string('progress/includes/partial_contract_list.html', {
                'ivts': ivts
            })

        else:
            data['form_is_valid'] = False
            print(form.errors)
    context = {'form': form}
    data['html_form'] = render_to_string(template_name, context, request=request)
    return JsonResponse(data)
"""Auxilary Function to Create Progress Item    """
def createProgreesItem(strucure_id):
    return 0
from .models import Contract_Intervention
from .auxilaryquery import creteProgressItem,deleteProgressItem
@login_required
def add_contract_Intervention(request):
    data = dict()
    if request.method == 'POST':
        print(request.POST)
        form = ContractAddForm(request.POST)
        # print(form)
        if form.is_valid():
            data['form_is_valid'] = True
            dpp_intervtion_id = form.cleaned_data.get("dpp_intervention_id")
            contract_id = form.cleaned_data.get("contract_id")
            status=Contract_Intervention.objects.filter(dpp_intervention_id=dpp_intervtion_id,contract_id=contract_id).exists()
            #civt_id=Contract_Intervention.objects.filter(dpp_intervention_id=dpp_intervtion_id,contract_id=contract_id)
            #contract_itevention = get_object_or_404(Contract_Intervention, dpp_intervention_id=dpp_intervtion_id)
            if not status:
                print("saving form")
                form.save()
                civt = Contract_Intervention.objects.filter(dpp_intervention_id=dpp_intervtion_id,contract_id=contract_id).first()
                dpp_intervention=civt.dpp_intervention_id
                print("Before add status is:{}".format(civt.dpp_intervention_id.contract_status))
                civt.dpp_intervention_id.contract_status="HAVE_CONTRACT"
                #dpp_intervention.Intervention_Status.contract_status="HAVE_CONTRACT"
                civt.dpp_intervention_id.save()
                #dpp_intervention2 = DPP_Intervention.objects.get(pk=dpp_intervtion_id.id)
                print("After add status is:{} ".format(civt.dpp_intervention_id.contract_status))
                print("id of saved contract intervention={}".format(civt.pk))
                creteProgressItem(civt)
            else:
                print("object already exists in database")

            data['form_is_valid'] = True
            # contract_id = form.fields["contract_id"]

            print("selected contract id={}".format(contract_id))
            ivts = Contract_Intervention.objects.filter(contract_id=contract_id)
            data['html_ivt_list'] = render_to_string('progress/includes/partial_contract_list.html', { 'ivts': ivts  })
            #print("curent contract intervention id".format(contract_itevention.id))




        else:
            data['form_is_valid'] = False
            print(form.errors)
    else:
        ivt_id = request.GET.get('ivt-id')
        contract_id = request.GET.get('contract')
        # form =  ContractAddForm(contract_id=contract_id, dpp_intervention_id=ivt_id)

        print("ivt_id={} contract_id={}".format(ivt_id, contract_id))
        form = ContractAddForm()
        form.fields["contract_id"].initial = contract_id
        # form.fields["contract_id"].disabled=True
        form.fields["dpp_intervention_id"].initial = ivt_id
        # form.fields["dpp_intervention_id"].disabled =True
        # print(form)
        context = {'form': form}
        data['html_form'] = render_to_string('progress/includes/partial_contract_create.html', context, request=request)
    return JsonResponse(data)
    # return save_contract_form(request, form, 'progress/includes/partial_contract_create.html')


from .models import Contract

@login_required
def list_contract_Intervention(request):
    haors = Haor.objects.all()
    contracts = Contract.objects.all().order_by('package_short_name')
    context = {'haors': haors, 'contracts': contracts}
    return render(request, 'progress/edit_drop_contract_intervention2.html', context)

@login_required
def update_contract_Intervention(request, pk):
    ivt = get_object_or_404(Contract_Intervention, pk=pk)
    data = dict()
    if request.method == 'POST':
        form = ContractAddForm(request.POST, instance=ivt)
        if form.is_valid():
            form.save()
            data['form_is_valid'] = True
            # contract_id = form.fields["contract_id"]
            contract_id = form.cleaned_data.get("contract_id")
            print("selected contract id={}".format(contract_id))
            ivts = Contract_Intervention.objects.filter(contract_id=contract_id)
            data['html_ivt_list'] = render_to_string('progress/includes/partial_contract_list.html', {
                'ivts': ivts
            })
        else:
            data['form_is_valid'] = False
            print(form.errors)
    else:
        form = ContractAddForm(instance=ivt)
        context = {'form': form}
        data['html_form'] = render_to_string('progress/includes/partial_contract_update_form.html', context,
                                             request=request)
    return JsonResponse(data)

@login_required
def delete_contract_Intervention(request, pk):
    ivt = get_object_or_404(Contract_Intervention, pk=pk)
    data = dict()
    if request.method == 'POST':
        # print("pk={}".format(pk))
        contract_id = ivt.contract_id
        print("contract id={}".format(contract_id))
        #deleteProgressItem(ivt)
        ivt.delete()
        data['form_is_valid'] = True

        ivts = Contract_Intervention.objects.filter(contract_id=contract_id)
        print("before:{}".format(ivt.dpp_intervention_id.contract_status))
        ivt.dpp_intervention_id.contract_status="HAVE_NO_CONTRACT"
        #dpp_ivt=DPP_Intervention.objects.get(pk=ivt.dpp_intervention_id)
        ivt.dpp_intervention_id.save()
        print("after:{}".format(ivt.dpp_intervention_id.contract_status))
        data['html_ivt_list'] = render_to_string('progress/includes/partial_contract_list.html', {
            'ivts': ivts
        })

    else:
        form = ContractAddForm(instance=ivt)
        context = {'form': form, 'ivt': ivt}
        data['html_form'] = render_to_string('progress/includes/partial_contract_delete_form.html', context,
                                             request=request)

    return JsonResponse(data)

@login_required
def list_contract_Intervention_sort_by_contract(request, pk):
    print(pk)
    ivts = Contract_Intervention.objects.filter(contract_id=pk)
    data = dict()
    data['html_ivt_list'] = render_to_string('progress/includes/partial_contract_list.html', {
        'ivts': ivts
    })

    return JsonResponse(data)

@login_required
def list_contract_Intervention_sort_by_contract_all(request):
    ivts = Contract_Intervention.objects.all().order_by('contract_id__package_short_name')
    data = dict()
    data['html_ivt_list'] = render_to_string('progress/includes/partial_contract_list.html', {
        'ivts': ivts
    })

    return JsonResponse(data)


"views for administration of progress Items"
"""$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"""
"""$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"""
"""$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"""
"""$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"""
"""$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"""
"""$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"""
"""$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"""
"""$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"""

@login_required
def list_progress_item(request):
    haors = Haor.objects.all()
    contracts = Contract.objects.all()
    civts = Contract.objects.all()
    context = {'haors': haors, 'contracts': contracts, 'civts': civts}
    return render(request, 'progress/add_edit_drop_progress_item.html', context)


from .forms import AddProgressItemForm
from .models import ProgressItem

@login_required
def create_progress_item(request):
    data = dict()
    if request.method == "POST":
        form = AddProgressItemForm(request.POST)
        if form.is_valid():
            form.save()
            data['form_is_valid'] = True
            civt_id = form.cleaned_data.get("intervention_id")
            print("structure name={}".format(civt_id))
            civts = ProgressItem.objects.filter(intervention_id=civt_id)

            data['html_ivt_list'] = render_to_string('progress/includes/progress_item/partial_progress_item_list.html',
                                                     {
                                                         'civts': civts})

        else:
            data['form_is_valid'] = False
            # print(form)
    else:

        form = AddProgressItemForm()
        contract_id = request.GET.get('contract')
        civt_id = request.GET.get('civt_id')
        # form =  ContractAddForm(contract_id=contract_id, dpp_intervention_id=ivt_id)
        print("the structure id={}".format(civt_id))
        form.fields["intervention_id"].initial = civt_id
        context = {'form': form}
        data['html_form'] = render_to_string('progress/includes/progress_item/partial_progress_item_create.html',
                                             context, request=request)
        # print(data)
    return JsonResponse(data)

@login_required
def update_progress_item(request, pk):
    ivt = get_object_or_404(ProgressItem, pk=pk)
    data = dict()
    if request.method == 'POST':
        form = AddProgressItemForm(request.POST, instance=ivt)
        if form.is_valid():
            form.save()
            data['form_is_valid'] = True
            # contract_id = form.fields["contract_id"]
            civt_id = form.cleaned_data.get("intervention_id")
            print("structure name={}".format(civt_id))
            civts = ProgressItem.objects.filter(intervention_id=civt_id)

            data['html_ivt_list'] = render_to_string('progress/includes/progress_item/partial_progress_item_list.html',
                                                     {
                                                         'civts': civts})
        else:
            data['form_is_valid'] = False
            print(form.errors)
    else:
        form = AddProgressItemForm(instance=ivt)
        context = {'form': form}
        data['html_form'] = render_to_string('progress/includes/progress_item/partial_progress_item_update.html',
                                             context, request=request)
    return JsonResponse(data)

@login_required
def delete_progress_item(request, pk):
    ivt = get_object_or_404(ProgressItem, pk=pk)
    data = dict()
    if request.method == 'POST':
        # print("pk={}".format(pk))
        civt_id = ivt.intervention_id
        print("intervention_id={}".format(civt_id))
        ivt.delete()
        data['form_is_valid'] = True

        civts = ProgressItem.objects.filter(intervention_id=civt_id)

        data['html_ivt_list'] = render_to_string('progress/includes/progress_item/partial_progress_item_list.html',
                                                 {
                                                     'civts': civts})

    else:
        form = AddProgressItemForm(instance=ivt)
        context = {'form': form}
        data['html_form'] = render_to_string('progress/includes/progress_item/partial_progress_item_delete.html',
                                             context, request=request)

    return JsonResponse(data)

@login_required
def get_contract_interventions(request, pk):
    print(pk)
    civts = Contract_Intervention.objects.filter(contract_id=pk)
    # civts=Contract_Intervention.objects.all()
    data = dict()
    data['html_civt_list'] = render_to_string('progress/includes/contract_intervention_select.html', {'civts': civts})
    print(data)
    return JsonResponse(data)

@login_required
def get_progress_item(request, pk):
    civts = ProgressItem.objects.filter(intervention_id=pk)


    data = dict()

    data['html_civt_list'] = render_to_string('progress/includes/progress_item/partial_progress_item_list.html',
                                              {'civts': civts})



    return JsonResponse(data)

@login_required
def update_progrss_quantity(request):
    civts=Contract_Intervention.objects.all()
    return render(request, 'progress/progress_quantity_update.html',    {'civts': civts})
@login_required
def update_progrss_quantity2(request,pk):
    civts=Contract_Intervention.objects.filter(contract_id=pk)
    return render(request, 'progress/progress_quantity_update.html',    {'civts': civts})


from .models import Progress_Quantity
from django.core.serializers import serialize
class progressQuantity():
    def __init__(self, pitem):
        self.item_id=pitem.id
        self.item_name=pitem.item_name
        self.totalquantity=pitem.quantity
        self.unit=pitem.unit
        self.weight=pitem.weight
        self.getCurrentQuantity()
    def getCurrentQuantity(self):
        quantites=Progress_Quantity.objects.filter(progress_item_id=self.item_id).order_by('-date','-id')
        if quantites:
            self.currentQuantity=quantites[0].quantity
            self.lastUpdateDate=quantites[0]. date
        else:
            self.currentQuantity =0
            self.lastUpdateDate="xxxx-xx-xx"





@login_required
def calculate_prgoressReport_Item(pk,user):
    pitems=ProgressItem.objects.filter(intervention_id=pk)
    mydata=serialize('json',pitems)
    for item in pitems:
        pquantity=get_object_or_404(Progress_Quantity,progress_item_id=item.id, user_id=user)
    return mydata



@login_required
def get_progress_report_items(request,pk):
    data=dict()
    #print("Structure Id={}".format(pk))
    civts = ProgressItem.objects.filter(intervention_id=pk)
    #print(civts)
    myitems=list()
    for civt in civts:
        myitem=progressQuantity(civt)
        myitems.append(myitem)
    #print(myitems)
    data['pitems']=render_to_string('progress/includes/progress_quantity/progress_quantity_list.html',{'civts':myitems},request=request)
    #print(data)
    images = ConstructionImage.objects.filter(structure_id=pk)
    print(images)
    firstimage = images.first()
    lastimage = images.last()
    context = {'images': images, 'firstimage': firstimage, 'lastimage': lastimage}
    data['image_carousel'] = render_to_string('progress/includes/construction_image/Carousel.html', context)
    return JsonResponse(data)
@login_required
def get_progress_report_items2(request,pk):
    data=dict()
    #print("Structure Id={}".format(pk))
    civts = ProgressItem.objects.filter(intervention_id=pk)
    #print(civts)
    myitems=list()
    for civt in civts:
        myitem=progressQuantity(civt)
        myitems.append(myitem)
    #print(myitems)
    data['pitems']=render_to_string('progress/includes/progress_quantity/progress_quantity_list.html',{'civts':myitems},request=request)
    #print(data)
    images = ConstructionImage.objects.filter(structure_id=pk)
    print(images)
    firstimage = images.first()
    lastimage = images.last()
    context = {'images': images, 'firstimage': firstimage, 'lastimage': lastimage}
    data['image_carousel'] = render_to_string('progress/includes/construction_image/carousel.html', context)
    return JsonResponse(data)



from .forms import ProgressQuantityForm
"""    
def input_progrss_quantity(request,pk):
    data=dict()
    print("pk={}".format(pk))
    if request.method=="POST":
        pass
    else:
        form=ProgressQuantityForm()
        form.fields["item_id"].initial = pk
        context = {'form': form}
        data['html_form'] = render_to_string('progress/includes/progress_quantity/progress_quantity_input.html',
                                             context, request=request)
        print(data)
        return JsonResponse(data)

"""
@login_required
def input_progrss_quantity(request,pk):
    data=dict()
    print("pk={}".format(pk))
    if request.method=="POST":
        pass
    else:
        form=ProgressQuantityForm()
        form.fields["item_id"].initial = pk
        context = {'form': form}
        data['html_form'] = render_to_string('progress/includes/progress_quantity/progress_quantity_input.html',
                                             context, request=request)
        #print(data)
        return JsonResponse(data)
@login_required
def input_progress_quantity_ajax(request):
    print(request)
    cid=request.GET.get('id')
    cid=int(cid)
    quantities=request.GET.get('quantity')
    print("quantities={} id={}".format(quantities,cid))
    pitem=get_object_or_404(ProgressItem,id=cid)
    print(pitem)
    myquantity=Progress_Quantity.objects.create(progress_item_id=pitem,quantity=quantities)
    myquantity.user_id=request.user
    myquantity.save()
    data=dict()
    print(myquantity)
    data['status']='success'
    return JsonResponse(data)
""" Report Generation Section  """
from .auxilaryquery import createProgressReport
from reportlab.pdfgen import canvas
from django.core.files.storage import FileSystemStorage
from reportlab.lib.enums import TA_JUSTIFY
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Image
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.units import inch
from django.core.files.storage import FileSystemStorage
@login_required
def progressReport(request):
    user=request.user
    print(user.profile.role.role_name)
    if user.profile.role.role_name =="XEN":
        contracts=Contract.objects.filter(xen_id=user.id)
        report_pdf=createProgressReport(contracts,user)
        #return render(request,'index/xen_index.html',{'contracts': contracts})
    elif(user.profile.role.role_name=="SP_ADMIN"):
        contracts = Contract.objects.all()
        report_pdf = createProgressReport(contracts,user)
        #return render(request, 'index/admin_index.html',{'contracts': contracts})
    elif (user.profile.role.role_name == "FIELD_ENGG_CONSL"):
        contracts = Contract.objects.filter(fse_id=user.id)
        report_pdf = createProgressReport(contracts,user)
        #return render(request, 'index/fse_index.html',{'contracts': contracts})
    elif (user.profile.role.role_name == "CSE_CONSL"):
        contracts = Contract.objects.filter(cse_id=user.id)
        report_pdf = createProgressReport(contracts,user)
        #return render(request, 'index/Cse_index.html',{'contracts': contracts})

    response=HttpResponse(content_type='application/pdf')
    response['Content-Disposition'] = 'inline;filename="somefilename.pdf" '
    response.write(report_pdf)
    return response
"""  
    #p=canvas.Canvas(response)
    #p.drawString(100, 100, "Hello world.")
    #p.showPage()
    #p.save()
    #response.write(report_pdf)
    doc = SimpleDocTemplate("/tmp/myfile.pdf")
    styles = getSampleStyleSheet()
    Story = [Spacer(1, 2 * inch)]
    style = styles["Normal"]
    for i in range(100):
        bogustext = ("This is Paragraph number %s.  " % i) * 20
        p = Paragraph(bogustext, style)
        Story.append(p)
        Story.append(Spacer(1, 0.2 * inch))
    doc.build(Story)
    fs = FileSystemStorage()
    filename = 'mypdf.pdf'


    fs = FileSystemStorage("/tmp")
    with fs.open("myfile.pdf") as pdf:
        response = HttpResponse(pdf, content_type='application/pdf')
        response['Content-Disposition'] = 'attachment; filename="myfile.pdf"'
        return response

    return response

 """

""""dash board"""
from django.shortcuts import HttpResponse
from .auxilaryquery import buildIdForContractCardLink,buildIdForContractCard,buildIdForContractCard2,getProgressData
def dashBoard(request):
    user = request.user
    if user.profile.role.role_name =="XEN":
        contracts=Contract.objects.filter(xen_id=user.id).order_by('package_short_name')
        #report_pdf=createProgressReport(contracts,user)
        #return render(request,'index/xen_index.html',{'contracts': contracts})
    elif(user.profile.role.role_name=="SP_ADMIN"):
        contracts = Contract.objects.all().order_by('package_short_name')
        #report_pdf = createProgressReport(contracts,user)
        #return render(request, 'index/admin_index.html',{'contracts': contracts})
    elif (user.profile.role.role_name == "FIELD_ENGG_CONSL"):
        contracts = Contract.objects.filter(fse_id=user.id).order_by('package_short_name')
        #report_pdf = createProgressReport(contracts,user)
        #return render(request, 'index/fse_index.html',{'contracts': contracts})
    elif (user.profile.role.role_name == "CSE_CONSL"):
        contracts = Contract.objects.filter(cse_id=user.id).order_by('package_short_name')
        #report_pdf = createProgressReport(contracts,user)
        #return render(request, 'index/Cse_index.html',{'contracts': contracts})
    clid = buildIdForContractCardLink(contracts)
    cid = buildIdForContractCard(contracts)
    items=buildIdForContractCard2(contracts)
    pdata = getProgressData(contracts)
    print(pdata)
    context={'contracts':items,'clid':clid,'cid':cid,'pdata':pdata}
    pdata=getProgressData(contracts)
    #return render(request, 'xen_dashboard.html', context)
    if user.profile.role.role_name=="SP_ADMIN":
        return render(request, 'pd_dashboard.html', context)
    else:
        return render(request, 'xen_dashboard_rev1.html', context)
from .auxilaryquery import calculatePackageProgressData2,fillBlankProgressQuantity

def dashBoard_Graph(request,pk):
    data=dict()
    data['category']=['Emb','SEmb' 'Riv','Khal','Reg/Box/Oulet','Iri']
    data['value'] = [10, 0, 20, 30, 100,100]
    #print(request)
    contract=Contract.objects.all()

    user = request.user
    #print(user)
   #print(contract)
    civts=list(Contract_Intervention.objects.filter(contract_id=pk))
    mydata=calculatePackageProgressData2(civts,user)
    #print(mydata['pdatas'])
    context={'pdatas':mydata['pdatas']}
    mytable=render_to_string('includes/progress_table.html',context, request=request)
    #print(mytable)
    mydata['tabledata']=mytable
    """    
    for ivt in civts:
        #print(ivt.id)
        pitems=list(ProgressItem.objects.filter(intervention_id=ivt.id))
        #print(pitems)
        for item in pitems:
            print(item)
            if user.profile.role.role_name == "XEN":
                pquantity = list(Progress_Quantity.objects.filter(progress_item_id=item.id,
                            user_id=user).order_by('-date')[:2])
                # report_pdf=createProgressReport(contracts,user)
                # return render(request,'index/xen_index.html',{'contracts': contracts})
                print(pquantity)
            elif (user.profile.role.role_name == "SP_ADMIN"):
                pquantity = list(Progress_Quantity.objects.filter(progress_item_id=item.id).order_by('-date')[:2])
                #contracts = Contract.objects.all().order_by('package_short_name')
                print(pquantity)
                # report_pdf = createProgressReport(contracts,user)
                # return render(request, 'index/admin_index.html',{'contracts': contracts})
            elif (user.profile.role.role_name == "FIELD_ENGG_CONSL"):
                pquantity = list(Progress_Quantity.objects.filter(progress_item_id=item.id,
                                                                  user_id=user).order_by('-date')[:2])
                print(pquantity)
                # report_pdf = createProgressReport(contracts,user)
                # return render(request, 'index/fse_index.html',{'contracts': contracts})
            elif (user.profile.role.role_name == "CSE_CONSL"):
                pquantity = list(Progress_Quantity.objects.filter(progress_item_id=item.id,
                                                                  user_id=user).order_by('-date')[:2])
                print(pquantity)

             
            if (user.profile.role.role_name == "SP_ADMIN"):
                
                
            
            pquantity = list(Progress_Quantity.objects.filter(progress_item_id=item.id, user_id=user).order_by('-date')[:2])
            print(pquantity)
            




    #print(civts)
     """

    #if Contract_Intervention.objects.filter(contract_id=contract).exists():
        #mydata=calculatePackageProgressData(contract,user)
    #else:
        #mydata=data
    #mydata=dict()
    #data['target']=mydata['mydata']
    #mydata = {'target': filteredTarget, 'achivment': fiteredProg, 'filteredItem': filteredItem}
    #print(mydata)
    #mydata = data
    return JsonResponse(mydata)
from .auxilaryquery import dummyProgressQuantity
def dashBoard_Graph2(request,pk):
    user = request.user
    # print(user)
    # print(contract)
    contract=Contract.objects.filter(id=pk)
    Progress_Quantity.objects.all().delete()
    ProgressItem.objects.all().delete()
    civts = list(Contract_Intervention.objects.filter(contract_id=pk))
    for ivt in civts:
        creteProgressItem(ivt)
    #fillBlankProgressQuantity(pitems, contract)
    #mydata=dummyProgressQuantity(civts,user)

    mydata = {'target': [0, 1, 2], 'achievement': [0, 1, 2], 'filteredItem': ['x', 'y', 'z'],
              'tenderd': [0, 1, 2], 'completed': [0, 1, 2]}
    #print("filling dummy for {} ".format(contract))
    return JsonResponse(mydata)

"""  
###############################################################################################
###############################################################################################
Create Progress Event
###############################################################################################
###############################################################################################
"""
from .forms import ReportEventForm
from .auxilaryquery import crerateProgressReportEventData

def createReportEvent(request):
    data=dict()
    if request.method=="POST":
        print("This is post request.....")
        form=ReportEventForm(request.POST)
        if form.is_valid():
            data['form_is_valid']=True
            crerateProgressReportEventData(form)
        else:
            data['form_is_valid'] =False
        #data['html_form'] = form
        #print(form)
    else:
        print("This is get request..........")
        form=ReportEventForm()
    context={'form':form}
    html_form = render_to_string('progress/includes/partial_report_event_create.html',context,
                                request=request,)
        #print(html_form)
    data['html_form']=html_form
    return JsonResponse(data)
""" View Report Event list"""
from .models import ReportEvent
def viewReportEvent2(request):
    data=dict()
    myreports=ReportEvent.objects.all()
    context={"Reports":myreports}
    data['html_list'] = render_to_string('includes/progress_event_table.html', context,
                                 request=request, )
    #print(data)
    return JsonResponse(data)
from .forms import ReportEventEditForm
from .models import ReportEvent
def editReportEvent(request,pk):
    data=dict()
    if request.method =="POST":
        print("This is a post request.........")
        print("Executing. function={}".format(editReportEvent.__name__))
        form=ReportEventEditForm(request.POST)
        #print(form)
        if form.is_valid():
            data['form_is_valid']=True
            eventslist=ReportEvent.objects.all()
            context={'Reports': eventslist}
            mylist=render_to_string('includes/progress_event_table.html', context,request, )
            data['event_list']= mylist
            #crerateProgressReportEventData(form)
        else:
            data['form_is_valid'] = False
    else:
        print("This is a get request....")
        form = ReportEventEditForm()

        #print(myreport.startingDate)

        #print(myform)
        #myform.set_InitiaValue(myreport)
        #print(myform)
        myreport = get_object_or_404(ReportEvent, pk=pk)
        context = {'form': form,"Report":myreport}
        data['html_form'] = render_to_string('progress/includes/partial_report_event_edit.html', context,request, )

        #print(data)
    return JsonResponse(data)
def editReportEvent2(request):
    data=dict()
    if request.method=="POST":
        print("This is post request.....")
        form=ReportEventForm()
        if form.is_valid():
            data['form_is_valid']=True
            #crerateProgressReportEventData(form)
        else:
            data['form_is_valid'] =False
        #data['html_form'] = form
        #print(form)
    else:
        print("This is get request..........")
        form=ReportEventEditForm()
    context={'form':form}
    html_form = render_to_string('progress/includes/partial_report_event_edit.html',context,
                                request=request,)
        #print(html_form)
    data['html_form']=html_form
    #print(data)
    return JsonResponse(data)


"""View Reports"""
from django.views.generic import ListView,View
class viewReportEvent(ListView):
    model=ReportEvent
    template_name='includes/report_index3.html'
    context_object_name='reports'
    """  
    data=dict()
    myreports=ReportEvent.objects.all()
    context={"Reports":myreports}
    #data['html_list'] = render_to_string('includes/progress_event_table.html', context,request=request, )


    #print(data)
    return render(request,'includes/report_index.html',context,)
    #return JsonResponse(data)
    """


from .auxilaryquery import crerateProgressReportEventData2,convertReportObject_to_dic

class createReportEvent2(View):
    def get(self, request):
        #crerateProgressReportEventData2
        data=dict()
        data['name'] = request.GET.get('name', None)
        data['stdate'] = request.GET.get('stdate', None)
        data['fdate'] = request.GET.get('fdate', None)
        data['subdate'] = request.GET.get('subdate', None)
        data['reportingPerson']=request.GET.getlist('persons[]', None)
        myreport=crerateProgressReportEventData2(data)
        print(myreport)
        #address1 = request.GET.get('address', None)
        #age1 = request.GET.get('age', None)

        #user = {'id': obj.id, 'name': obj.name, 'address': obj.address, 'age': obj.age}

        #data = {'user': name1        }
        #returnData=convertReportObject_to_dic(myreport)
        myreports = ReportEvent.objects.all()
        context={"reports":myreports}
        html_table_body = render_to_string("includes/message_table_body.html", context,request=request, )
        #print(html_table_body)
        returnData=dict()
        returnData["tbody"]=html_table_body
        return JsonResponse(returnData)

"""views for qualitative progress """
from .models import qualitativeStatus
def Qualitative_progress(request):
    contracts=Contract.objects.all().order_by('package_short_name')
    structures = qualitativeStatus.objects.all().order_by('contract_ivt__contract_id__package_short_name',
                                                          'contract_ivt__dpp_intervention_id__worktype_id')
    context={"structures":structures,"contracts":contracts}
    return render(request,"progress/qualitative_progress_list2.html",context)

from .models import Division

def Qualitative_progress_sort(request):
    #print("sucessfully trapped sort events for progress update.....")
    data = dict()
    divisions=["KISH-ALL","HOBI-ALL","NETR-ALL","SUNM-DIV-I-ALL","SUNM-DIV-II-ALL" ]
    div_index=[1,6,3,4,5]
    whole_project=["WHOLE-PROJECT"]
    search_id= request.GET['contract_id']
    if search_id in divisions:
        idx=divisions.index(search_id)
        div_pr_key=div_index[idx]
        mydivision=get_object_or_404(Division,pk=div_pr_key)
        #print("Now searching division {}".format(mydivision.division_name))
        #print(div_pr_key)
        #contrats=Contract.objects.all().filter(division_id=mydivision.pk)
        #print(contrats)
        structures = qualitativeStatus.objects.all().filter(contract_ivt__contract_id__division_id=mydivision).order_by('contract_ivt__contract_id__package_short_name','contract_ivt__dpp_intervention_id__worktype_id')
        #structures=qualitativeStatus.objects.select_related('contract_ivt__contract_id__division_id').all()
        print(structures)
    elif search_id in whole_project:
        #structures = qualitativeStatus.objects.all().order_by('contract_ivt__contract_id__package_short_name')
        structures = qualitativeStatus.objects.all().order_by('contract_ivt__contract_id__package_short_name','contract_ivt__dpp_intervention_id__worktype_id')
    else:
        contract_package=get_object_or_404(Contract,pk=search_id)
        print("package name={}".format(contract_package.package_short_name))
        structures = qualitativeStatus.objects.all().filter(contract_ivt__contract_id=contract_package).order_by('contract_ivt__dpp_intervention_id__worktype_id')
    context={'structures':structures}

    table_data=render_to_string('progress/includes/structures/partial_qualitative_progress_list.html',context,request=request)
    #print(table_data)
    #print(contract_id)
    #print(structures)
    data['html_ivt_list']=table_data
    return JsonResponse(data)

    #structures = qualitativeStatus.objects.all().order_by('contract_ivt__contract_id')
    #context={"structures":structures}
    #return render(request,"progress/qualitative_progress_list2.html",context)

from .forms import qprogresForm
def Qualitative_progress_update(request,pk):
    ivt = get_object_or_404(qualitativeStatus, pk=pk)
    print(ivt)
    data=dict()

    if request.method == 'POST':
        form = qprogresForm(request.POST, instance=ivt)
        if form.is_valid():
            form.save()
            contract_package=ivt.contract_ivt.contract_id
            #structures = qualitativeStatus.objects.all().order_by('contract_ivt__contract_id')
            structures = qualitativeStatus.objects.all().filter(contract_ivt__contract_id=contract_package).order_by('contract_ivt__dpp_intervention_id__worktype_id')
            #.order_by('contract_ivt__dpp_intervention_id__worktype_id ')
            context = {'structures': structures}
            table_data = render_to_string('progress/includes/structures/partial_qualitative_progress_list.html',
                                          context, request=request)
            data['form_is_valid']=True
            data['html_ivt_list']=table_data
        else:
            data['form_is_valid'] = True

    else:
        form = qprogresForm(instance=ivt)
        context={'form':form}
        data['html_form']=render_to_string('progress/includes/structures/partial_ivt_update_form.html',context,request=request)
    return JsonResponse(data)

""""Map Related views goes here"""
from django.core.serializers import serialize
from .models import Haor

def BlankMap(request):
    #haor_data=get_object_or_404(Haor,pk=2)
    haor_data=Haor.objects.all().order_by('pk')
    #print(haor_data)
    #mydata=serialize('geojson',Haor.objects.all(),geometry_field='boundary',fields=('name','project_type',))
    #mydata = serialize('geojson', haor_data, geometry_field='boundary', fields=('name', 'project_type',))
    #print(mydata)
    context={"haors":haor_data}
    return render(request,'progress/includes/maps/blank_map_Leaflet.html',context)

from django.contrib.gis.db.models.functions import Centroid
from django.contrib.gis.geos import fromstr,Polygon,GEOSGeometry,fromfile,fromstr,MultiPolygon,Point
from .auxilaryquery import buildMarkerData

def HaorMap(request,pk):
    data=dict()
    myid=request.GET['haorid']
    print("haor_id={}".format(myid))
    #haor_data=get_object_or_404(Haor,pk=2)
    haor_data = Haor.objects.all().filter(pk=myid)

    for haor in haor_data:
        haor_name=haor
        mycenter=haor.boundary.centroid
        mycenter2=Point(mycenter.x,mycenter.y)
        myboundary = haor.boundary
        #print(myboundary)
        mycoords=myboundary.coords
        for p in myboundary:
            a=p.area
            #geom_typeid
            print("area={} type={}".format(a,p.geom_typeid))
            mypoly=p.geojson
            data['boundary']=mypoly
            data['boundary_coords']=p.coords
            print(mypoly)
            #print(p)
            #print(mycoords)
        #print(mycenter.x)

    #print(haor_data)
    # mydata=serialize('geojson',Haor.objects.all(),geometry_field='boundary',fields=('name','project_type',))

    #mydata = serialize('geojson', haor_data, geometry_field='boundary', fields=('name', 'project_type',))
    #centroid=serialize('geojson',mycenter2)
    #print(centroid)
    #print(mydata)
    #data['boundary']=mydata
    #data['centroid']=centroid
    structures=Contract_Intervention.objects.all().filter(dpp_intervention_id__haor_id=haor_name)
    #filter(dpp_intervention_id__haor_id=haor_data)
    #print(structures)
    myvalues=buildMarkerData(structures)
    data['lats']=myvalues['lats']
    data['lons']=myvalues['lons']
    data['names']=myvalues['names']
    data['current_status']=myvalues['current_status']
    data['present_progress']=myvalues['present_progress']
    print(myvalues)

    return JsonResponse(data)

    #return render(request, 'progress/includes/maps/blank_map.html')



"""IPC Related Views"""
def AddIPCQuantity(request):
    contracts=Contract.objects.all().order_by('package_short_name')
    context={"contracts":contracts}
    return render(request,'progress/includes/ipc/addIPCQuantity.html',context)

from .auxilaryquery import build_structure_select_options

def AddIPCQuantity_PcakageSort(request):
    packageName=request.GET['package_name']
    mycontract= Contract.objects.get(pk=packageName)
    print(mycontract)
    structures=Contract_Intervention.objects.all().filter(contract_id=mycontract)
    structure_options=build_structure_select_options(structures)
    print(structure_options)
    context={"structures":structures}
    data=dict()
    data['ids']=structure_options['ids']
    data['package_names']=structure_options['package_names']
    #data['structure_list']=render_to_string('progress/includes/ipc/structure_select.html',context,request=request)
    return JsonResponse(data)

from .models import BoQ,IPC
from .auxilaryquery import build_ipc_quantity
def AddIPCQuantity_LoadBoq(request):
    structure_id = request.GET['structureid']
    structure=get_object_or_404(Contract_Intervention,pk=structure_id)
    boq_objects=BoQ.objects.all().filter(structure_id=structure)
    current_contract=structure.contract_id
    print("contract={}".format(current_contract))
    ipc_objects=IPC.objects.all().filter(contract=current_contract)
    ipc_items=build_ipc_quantity(boq_objects,ipc_objects)
    context={"ipcitems": ipc_items}
    data=dict()
    data['ipc_quantity']=render_to_string('progress/includes/ipc/partial_ipc_item_list.html',context,request=request)
    return JsonResponse(data)










