#!/bin/bash

while (( "$#" )); do
  case "$1" in
    -g|--resource-group)
      RG=$2
      shift 2
      ;;
    -l|--location)
      location=$2
      shift 2
      ;;
    -s|--subscription)
      subscription=$2
      shift 2
      ;;
    -h|--help)
      echo "Usage: ./create_infrastructure.sh -g {Resource Group} -l {location} -s {Subscription Name}"
      exit 0
      ;;
    --) 
      shift
      break
      ;;
    -*|--*=) 
      echo "Error: Unsupported flag $1" >&2
      exit 1
      ;;
  esac
done

pushd `pwd`

appName=`cat /dev/urandom | tr -dc 'a-z' | fold -w 8 | head -n 1`

echo "Generating Application Name - ${appName}"

apimName=${appName}-api
functionAppName=func${appName}001
funcStorageName=sa${appName}001

az account show  >> /dev/null 2>&1
if [[ $? -ne 0 ]]; then
  az login
fi

#Get Subscription Id
az account set -s ${subscription}
subId=`az account show -o tsv --query id`

#Create Resource Group
az group create -n ${RG} -l ${location}

# Create an Azure Function with storage accouunt in the resource group.
if ! `az functionapp show --name ${functionAppName} --resource-group ${RG} -o none`
then
    az storage account create --name ${funcStorageName} --location ${location} --resource-group ${RG} --sku Standard_LRS
    az functionapp create --name ${functionAppName} \
		--storage-account ${funcStorageName} \
		--consumption-plan-location ${location} \
		--resource-group ${RG} \
		--functions-version 3 \
		--runtime node \
		--runtime-version 12 \
		--os-type linux
fi

az apim create -n ${apimName} -g ${RG} -l ${location} --sku-name Consumption --publisher-email brian@bjdcsa.cloud --publisher-name bjdcsa.cloud

cd ../src/mock-api
func azure functionapp publish ${functionAppName}
popd

# echo Application name
if [[ $? -eq 0 ]]; then
  cd ..
  echo ------------------------------------
  echo "Infrastructure built successfully. Application Name: ${appName}"
  echo ------------------------------------
else
  cd ..
  echo ------------------------------------
  echo "Errors encountered while building infrastructure. Please review. Application Name: ${appName}"
  echo ------------------------------------
fi
